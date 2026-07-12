using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Application.Database.Breach;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

using Qualifier.Domain.Interfaces;
namespace Qualifier.Application.Database.ControlEvaluation.Commands.UpdateControlEvaluation
{
    public class UpdateControlEvaluationCommand : IUpdateControlEvaluationCommand
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        private readonly IControlEvaluationRepository _controlEvaluationRepository;
        private readonly MaturityLevelBreachGenerator _maturityLevelBreachGenerator;

        public UpdateControlEvaluationCommand(IDatabaseService databaseService, IMapper mapper, IControlEvaluationRepository controlEvaluationRepository, MaturityLevelBreachGenerator maturityLevelBreachGenerator)
        {
            _databaseService = databaseService;
            _mapper = mapper;
            _controlEvaluationRepository = controlEvaluationRepository;
            _maturityLevelBreachGenerator = maturityLevelBreachGenerator;
        }

        public async Task<Object> Execute(UpdateControlEvaluationDto model, int id)
        {
            try
            {
                Notification notification = this.updateValidation(model);
                if (notification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(notification.errors);

                Notification existsNotification = await this.existsValidationAsync(id);
                if (existsNotification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(existsNotification.errors);

                // Se lee antes del Update porque el repositorio usa Attach+IsModified: una vez
                // guardado ya no se puede recuperar el maturityLevelId anterior para saber si
                // cambió (y por lo tanto si corresponde generar/actualizar una brecha).
                var previous = await _databaseService.ControlEvaluation
                    .Where(x => x.controlEvaluationId == id)
                    .Select(x => new { x.maturityLevelId, x.evaluationId, x.standardId, x.controlId, x.companyId })
                    .FirstOrDefaultAsync();

                await _controlEvaluationRepository.Update(id, _mapper.Map<ControlEvaluationEntity>(model));

                if (model.referenceDocumentations != null)
                {

                    await _databaseService.ReferenceDocumentation.Where(c => c.controlEvaluationId == id).ExecuteDeleteAsync();

                    foreach (var item in model.referenceDocumentations)
                    {
                        item.companyId = model.companyId;
                        var entity2 = _mapper.Map<ReferenceDocumentationEntity>(item);
                        entity2.controlEvaluationId = id;
                        entity2.evaluationId = item.evaluationId;
                        entity2.creationDate = DateTime.UtcNow;
                        entity2.creationUserId = model.updateUserId;
                        await _databaseService.ReferenceDocumentation.AddAsync(entity2);
                        await _databaseService.SaveAsync();
                    }

                }

                if (previous != null && model.maturityLevelId != null && model.maturityLevelId != previous.maturityLevelId)
                {
                    await createBreachFromControlEvaluationUpdate(previous.evaluationId, previous.standardId,
                        previous.controlId, model, previous.companyId);
                }

                return model;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

        private async Task createBreachFromControlEvaluationUpdate(int evaluationId, int standardId, int controlId,
            UpdateControlEvaluationDto model, int companyId)
        {
            int? severity = await _maturityLevelBreachGenerator.ResolveBreachSeverityIdAsync(model.maturityLevelId);
            if (severity == null)
                return;

            var existingBreach = await _maturityLevelBreachGenerator.GetOpenBreachAsync(
                evaluationId, MaturityLevelBreachGenerator.BREACH_TYPE_CONTROL, controlId, requirementId: 0);

            if (existingBreach != null)
            {
                await _maturityLevelBreachGenerator.SyncOpenBreachSeverityAsync(existingBreach, severity.Value);
                return;
            }

            var groups = await (from item in _databaseService.ControlGroup
                                where ((item.isDeleted == null || item.isDeleted == false) && item.standardId == standardId)
                                select new ControlGroupEntity
                                {
                                    controlGroupId = item.controlGroupId,
                                    number = item.number,
                                    name = item.name,
                                }).OrderBy(x => x.number)
                      .ToListAsync();

            var controls = await (from item in _databaseService.Control
                                  where ((item.isDeleted == null || item.isDeleted == false) && item.standardId == standardId)
                                  select new ControlEntity
                                  {
                                      controlId = item.controlId,
                                      controlGroupId = item.controlGroupId,
                                      number = item.number,
                                      name = item.name,
                                  }).OrderBy(x => x.number)
                              .ToListAsync();

            setControlsWithGroup(groups, controls);

            var control = controls.Where(c => c.controlId == controlId).FirstOrDefault();

            var numerationToShow = "";
            if (control != null)
                numerationToShow = control.numerationToShow;

            var title = control != null
                ? $"El control {control.numerationToShow} no se cumple: {control.name}"
                : $"El control {controlId} no se cumple";

            var breach = new BreachEntity
            {
                evaluationId = evaluationId,
                standardId = standardId,
                requirementId = 0,
                controlId = controlId,
                type = MaturityLevelBreachGenerator.BREACH_TYPE_CONTROL,
                title = title,
                description = model.justification ?? "El control no cumple con el nivel de madurez esperado.",
                breachSeverityId = severity.Value,
                breachStatusId = MaturityLevelBreachGenerator.BREACH_STATUS_OPEN,
                responsibleId = model.responsibleId ?? 0,
                numerationToShow = numerationToShow,
                evidenceDescription = model.improvementActions,
                companyId = companyId,
                creationUserId = model.updateUserId,
                creationDate = DateTime.UtcNow
            };

            await _databaseService.Breach.AddAsync(breach);
            await _databaseService.SaveAsync();
        }

        private void setControlsWithGroup(List<ControlGroupEntity> groups, List<ControlEntity> controls)
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

        private async Task<Notification> existsValidationAsync(int id)
        {
            Notification notification = new Notification();
            int total = await _databaseService.ControlEvaluation.CountAsync(item => item.controlEvaluationId == id);
            if (total == 0)
                notification.addError("El Id " + id.ToString() + " no se encuentra registrado");
            return notification;
        }

        private Notification updateValidation(UpdateControlEvaluationDto request)
        {
            Notification notification = new Notification();
            request.requiredFieldsValidation(notification);
            return notification;
        }

    }
}

