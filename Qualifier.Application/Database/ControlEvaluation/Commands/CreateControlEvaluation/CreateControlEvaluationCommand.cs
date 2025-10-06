using System.Transactions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.ControlEvaluation.Commands.CreateControlEvaluation

{
    public class CreateControlEvaluationCommand : ICreateControlEvaluationCommand
    {

        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public CreateControlEvaluationCommand(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
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
                    var entity = _mapper.Map<ControlEvaluationEntity>(model);
                    entity.creationDate = DateTime.UtcNow;
                    entity.creationUserId = model.creationUserId;
                    await _databaseService.ControlEvaluation.AddAsync(entity);
                    await _databaseService.SaveAsync();

                    long controlEvaluationId = entity.controlEvaluationId;

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

                    if (entity.maturityLevelId == MATURITY_INITIAL || entity.maturityLevelId == MATURITY_NOT_IMPLEMENTED)
                        await createBreachFromControlEvaluation(entity, model);

                    scope.Complete();
                }

                return model;
            }
            catch (Exception ex)
            {
                throw ex;
                //return BaseApplication.getExceptionErrorResponse();
            }
        }

        private Notification createValidation(CreateControlEvaluationDto request)
        {
            Notification notification = new Notification();
            request.requiredFieldsValidation(notification);
            return notification;
        }

        // Maturity levels
        private const int MATURITY_INITIAL = 5;
        private const int MATURITY_NOT_IMPLEMENTED = 6;

        // Breach severities
        private const int SEVERITY_LOW = 1;
        private const int SEVERITY_MEDIUM = 2;
        private const int SEVERITY_HIGH = 3;
        private const int SEVERITY_CRITICAL = 4;

        // Breach statuses
        private const int BREACH_STATUS_OPEN = 1;

        private async Task createBreachFromControlEvaluation(
    ControlEvaluationEntity entity,
    CreateControlEvaluationDto model)
        {
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

            int? severity = null;

            if (entity.maturityLevelId == MATURITY_INITIAL)
                severity = SEVERITY_MEDIUM;
            else if (entity.maturityLevelId == MATURITY_NOT_IMPLEMENTED)
                severity = SEVERITY_HIGH;

            if (severity != null)
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
                    breachStatusId = BREACH_STATUS_OPEN,
                    responsibleId = model.responsibleId,
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

