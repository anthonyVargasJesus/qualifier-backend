using System.Transactions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Application.Database.Breach;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.ControlEvaluation.Commands.CreateControlEvaluation

{
    public class CreateControlEvaluationCommand : ICreateControlEvaluationCommand
    {

        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        private readonly MaturityLevelBreachGenerator _maturityLevelBreachGenerator;

        public CreateControlEvaluationCommand(IDatabaseService databaseService, IMapper mapper, MaturityLevelBreachGenerator maturityLevelBreachGenerator)
        {
            _databaseService = databaseService;
            _mapper = mapper;
            _maturityLevelBreachGenerator = maturityLevelBreachGenerator;
        }

        public async Task<Object> Execute(CreateControlEvaluationDto model)
        {
            try
            {
                Notification notification = this.createValidation(model);
                if (notification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(notification.errors);

                using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    // El cliente decide POST vs PUT según lo que tenía cargado en memoria; si ese
                    // estado quedó desincronizado (reinicio de la app, dos guardados casi
                    // simultáneos, etc.) y ya existe una fila real para este control+evaluationId,
                    // se actualiza esa fila en vez de insertar un duplicado (antes esto creaba
                    // MAE_CONTROL_EVALUATION duplicados para el mismo control).
                    // OrderByDescending: no debería haber más de una fila por (controlId,
                    // evaluationId), pero no hay constraint único que lo garantice — si llegan a
                    // existir duplicados, se actualiza la más reciente (mismo criterio que
                    // ControlEntity.setEvaluations/GetActionPlansByUserIdQuery) en vez de una
                    // fila arbitraria.
                    var entity = await _databaseService.ControlEvaluation
                        .Where(x => x.controlId == model.controlId && x.evaluationId == model.evaluationId
                            && (x.isDeleted == null || x.isDeleted == false))
                        .OrderByDescending(x => x.controlEvaluationId)
                        .FirstOrDefaultAsync();

                    bool isNew = entity == null;
                    if (isNew)
                    {
                        entity = _mapper.Map<ControlEvaluationEntity>(model);
                        entity.creationDate = DateTime.UtcNow;
                        entity.creationUserId = model.creationUserId;
                        await _databaseService.ControlEvaluation.AddAsync(entity);
                    }
                    else
                    {
                        entity.maturityLevelId = model.maturityLevelId ?? entity.maturityLevelId;
                        entity.value = model.value ?? entity.value;
                        entity.responsibleId = model.responsibleId ?? entity.responsibleId;
                        entity.justification = model.justification ?? entity.justification;
                        entity.improvementActions = model.improvementActions ?? entity.improvementActions;
                        entity.controlDescription = model.controlDescription ?? entity.controlDescription;
                        entity.controlType = model.controlType ?? entity.controlType;
                        entity.updateDate = DateTime.UtcNow;
                        entity.updateUserId = model.creationUserId;
                    }
                    await _databaseService.SaveAsync();

                    long controlEvaluationId = entity.controlEvaluationId;
                    // Sin esto, el frontend recibe de vuelta el mismo id=0 que mandó y nunca se
                    // entera de que ya existe una fila real; el próximo guardado volvería a crear
                    // otra en vez de actualizar la que se acaba de insertar.
                    model.controlEvaluationId = controlEvaluationId;

                    if (model.referenceDocumentations != null)
                        foreach (var item in model.referenceDocumentations)
                        {
                            item.companyId = entity.companyId;
                            var entity2 = _mapper.Map<ReferenceDocumentationEntity>(item);
                            entity2.controlEvaluationId = controlEvaluationId;
                            entity2.requirementEvaluationId = null;
                            entity2.evaluationId = entity.evaluationId;
                            entity2.creationDate = DateTime.UtcNow;
                            entity2.creationUserId = model.creationUserId;
                            await _databaseService.ReferenceDocumentation.AddAsync(entity2);
                            await _databaseService.SaveAsync();
                        }

                    await createBreachFromControlEvaluation(entity, model);

                    scope.Complete();
                }

                return model;
            }
            catch (Exception)
            {
             return BaseApplication.getExceptionErrorResponse();
            }
        }

        private Notification createValidation(CreateControlEvaluationDto request)
        {
            Notification notification = new Notification();
            request.requiredFieldsValidation(notification);
            return notification;
        }

        private async Task createBreachFromControlEvaluation(
    ControlEvaluationEntity entity,
    CreateControlEvaluationDto model)
        {
            int? severity = await _maturityLevelBreachGenerator.ResolveBreachSeverityIdAsync(entity.maturityLevelId);
            if (severity == null)
                return;

            var existingBreach = await _maturityLevelBreachGenerator.GetOpenBreachAsync(
                entity.evaluationId, MaturityLevelBreachGenerator.BREACH_TYPE_CONTROL, entity.controlId, requirementId: 0);

            if (existingBreach != null)
            {
                await _maturityLevelBreachGenerator.SyncOpenBreachSeverityAsync(existingBreach, severity.Value);
                return;
            }

            var groups = await (from item in _databaseService.ControlGroup
                                where ((item.isDeleted == null || item.isDeleted == false) && item.standardId == model.standardId)
                                select new ControlGroupEntity
                                {
                                    controlGroupId = item.controlGroupId,
                                    number = item.number,
                                    name = item.name,
                                }).OrderBy(x => x.number)
                      .ToListAsync();

            var controls = await (from item in _databaseService.Control
                                  where ((item.isDeleted == null || item.isDeleted == false) && item.standardId == model.standardId)
                                  select new ControlEntity
                                  {
                                      controlId = item.controlId,
                                      controlGroupId = item.controlGroupId,
                                      number = item.number,
                                      name = item.name,
                                  }).OrderBy(x => x.number)
                              .ToListAsync();

            setControlsWithGroup(groups, controls);

            {
                var control = controls
                    .Where(c => c.controlId == entity.controlId)
                    .FirstOrDefault();

                var numerationToShow = "";
                if (control != null)
                    numerationToShow = control.numerationToShow;

                var title = control != null
                    ? $"El control {control.numerationToShow} no se cumple: {control.name}"
                    : $"El control {entity.controlId} no se cumple";

                var breach = new BreachEntity
                {
                    evaluationId = entity.evaluationId,
                    standardId = entity.standardId,
                    requirementId = 0, // 👈 Ahora es control, no requisito
                    controlId = entity.controlId,
                    type = "2", // 👈 puedes usar "1" = requisito, "2" = control (o un enum/string)
                    title = title,
                    description = model.justification ?? "El control no cumple con el nivel de madurez esperado.",
                    breachSeverityId = severity.Value,
                    breachStatusId = MaturityLevelBreachGenerator.BREACH_STATUS_OPEN,
                    responsibleId = model.responsibleId.Value,
                    numerationToShow = numerationToShow,
                    evidenceDescription = model.improvementActions,
                    companyId = model.companyId,
                    creationUserId = model.creationUserId,
                    creationDate = DateTime.UtcNow
                };

                await _databaseService.Breach.AddAsync(breach);
                await _databaseService.SaveAsync();
            }
        }

        public void setControlsWithGroup(List<ControlGroupEntity> groups, List<ControlEntity> controls)
        {
            foreach (var group in groups)
            {
                group.controls = controls.Where(x => x.controlGroupId == group.controlGroupId).OrderBy(x => x.number).ToList();
                setNumeration(group.controls, group.number);
            }

        }

        private void setNumeration(List<ControlEntity> controls, int parentNumber)
        {
            foreach (ControlEntity item in controls)
                item.numerationToShow = parentNumber.ToString() + "." + item.number.ToString();
        }

    }
}

