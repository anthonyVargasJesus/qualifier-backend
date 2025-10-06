using System.Transactions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;
using Qualifier.Domain.Interfaces;

namespace Qualifier.Application.Database.RequirementEvaluation.Commands.CreateRequirementEvaluation

{
    public class CreateRequirementEvaluationCommand : ICreateRequirementEvaluationCommand
    {

        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        private readonly IEvaluationRepository _evaluationRepository;

        public CreateRequirementEvaluationCommand(IDatabaseService databaseService, IMapper mapper, IEvaluationRepository evaluationRepository)
        {
            _databaseService = databaseService;
            _mapper = mapper;
            _evaluationRepository = evaluationRepository;
        }
        public async Task<Object> Execute(CreateRequirementEvaluationDto model)
        {
            try
            {
                Notification notification = this.createValidation(model);
                if (notification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(notification.errors);

                using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var entity = _mapper.Map<RequirementEvaluationEntity>(model);
                    const int PENDING_AUDITOR_STATUS_VALUE = 1;
                    entity.auditorStatus = PENDING_AUDITOR_STATUS_VALUE;
                    entity.creationDate = DateTime.UtcNow;
                    entity.creationUserId = model.creationUserId;
                    await _databaseService.RequirementEvaluation.AddAsync(entity);
                    await _databaseService.SaveAsync();
                    long requirementEvaluationId = entity.requirementEvaluationId;

                    if (model.referenceDocumentations != null)
                        foreach (var item in model.referenceDocumentations)
                        {
                            item.companyId = entity.companyId;

                            var entity2 = _mapper.Map<ReferenceDocumentationEntity>(item);
                            entity2.requirementEvaluationId = requirementEvaluationId;
                            entity2.evaluationId = entity.evaluationId;
                            entity2.creationDate = DateTime.UtcNow;
                            entity2.creationUserId = model.creationUserId;
                            await _databaseService.ReferenceDocumentation.AddAsync(entity2);
                            await _databaseService.SaveAsync();
                        }

                    const int EDITION_EVALUATION_STATE_ID = 2;
                    await _evaluationRepository.UpdateState(entity.evaluationId, EDITION_EVALUATION_STATE_ID);

                    if (entity.maturityLevelId == MATURITY_INITIAL || entity.maturityLevelId == MATURITY_NOT_IMPLEMENTED)
                        await createBreachFromEvaluation(entity, model);

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

        private Notification createValidation(CreateRequirementEvaluationDto request)
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

        private async Task createBreachFromEvaluation(
            RequirementEvaluationEntity entity,
            CreateRequirementEvaluationDto model)
        {
            var requirements = await (from requirement in _databaseService.Requirement
                                      where ((requirement.isDeleted == null || requirement.isDeleted == false)
                                      && requirement.standardId == model.standardId
                                      && requirement.isEvaluable)
                                      select new RequirementEntity
                                      {
                                          requirementId = requirement.requirementId,
                                          numeration = requirement.numeration,
                                          name = requirement.name,
                                          description = requirement.description,
                                          level = requirement.level,
                                          parentId = requirement.parentId,
                                      }).ToListAsync();

            var standardEntity = new StandardEntity();
            standardEntity.setRequirementsWithChildren(requirements);

            int? severity = null;

            if (entity.maturityLevelId == MATURITY_INITIAL)
                severity = SEVERITY_MEDIUM;
            else if (entity.maturityLevelId == MATURITY_NOT_IMPLEMENTED)
                severity = SEVERITY_HIGH;

            if (severity != null)
            {
                var requirement = requirements.Where(r => r.requirementId == entity.requirementId).FirstOrDefault();

                var numerationToShow = "";
                if (requirement != null)
                    numerationToShow = requirement.numerationToShow;

                var title = requirement != null
     ? $"El requisito {requirement.numerationToShow} no se cumple: {requirement.name}"
     : $"El requisito {entity.requirementId} no se cumple";

                var breach = new BreachEntity
                {
                    evaluationId = entity.evaluationId,
                    standardId = entity.standardId,
                    requirementId = entity.requirementId,
                    controlId = 0,
                    type = "1",
                    title = title,
                    description = model.justification ?? "El requisito no cumple con el nivel de madurez esperado.",
                    breachSeverityId = severity.Value,
                    breachStatusId = BREACH_STATUS_OPEN, // 👈 Always starts as Open
                    responsibleId = model.responsibleId ?? 0,
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



    }
}

