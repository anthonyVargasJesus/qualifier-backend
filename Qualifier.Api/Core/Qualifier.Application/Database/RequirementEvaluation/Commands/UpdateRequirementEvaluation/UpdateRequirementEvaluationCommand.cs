using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Application.Database.Breach;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

using Qualifier.Domain.Interfaces;
namespace Qualifier.Application.Database.RequirementEvaluation.Commands.UpdateRequirementEvaluation
{
    public class UpdateRequirementEvaluationCommand : IUpdateRequirementEvaluationCommand
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        private readonly IRequirementEvaluationRepository _requirementEvaluationRepository;
        private readonly IEvaluationRepository _evaluationRepository;
        private readonly MaturityLevelBreachGenerator _maturityLevelBreachGenerator;

        public UpdateRequirementEvaluationCommand(IDatabaseService databaseService, IMapper mapper, IRequirementEvaluationRepository requirementEvaluationRepository, IEvaluationRepository evaluationRepository, MaturityLevelBreachGenerator maturityLevelBreachGenerator)
        {
            _databaseService = databaseService;
            _mapper = mapper;
            _requirementEvaluationRepository = requirementEvaluationRepository;
            _evaluationRepository = evaluationRepository;
            _maturityLevelBreachGenerator = maturityLevelBreachGenerator;
        }

        public async Task<Object> Execute(UpdateRequirementEvaluationDto model, int id)
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
                var previous = await _databaseService.RequirementEvaluation
                    .Where(x => x.requirementEvaluationId == id)
                    .Select(x => new { x.maturityLevelId, x.evaluationId, x.standardId, x.requirementId, x.companyId })
                    .FirstOrDefaultAsync();

                await _requirementEvaluationRepository.Update(id, _mapper.Map<RequirementEvaluationEntity>(model));


                if (model.referenceDocumentations != null)
                {

                    await _databaseService.ReferenceDocumentation.Where(c => c.requirementEvaluationId == id).ExecuteDeleteAsync();

                    foreach (var item in model.referenceDocumentations)
                    {
                        item.companyId = model.companyId;
                        var entity2 = _mapper.Map<ReferenceDocumentationEntity>(item);
                        entity2.requirementEvaluationId = id;
                        entity2.evaluationId = item.evaluationId;
                        entity2.creationDate = DateTime.UtcNow;
                        entity2.creationUserId = model.updateUserId;
                        await _databaseService.ReferenceDocumentation.AddAsync(entity2);
                        await _databaseService.SaveAsync();
                    }

                }

                var requirementEvaluation = await (from item in _databaseService.RequirementEvaluation
                                                   where ((item.isDeleted == null || item.isDeleted == false) && item.requirementEvaluationId == id)
                                                   select new RequirementEvaluationEntity()
                                                   {
                                                       evaluationId = item.evaluationId,
                                                   }).FirstOrDefaultAsync();

                const int EDITION_EVALUATION_STATE_ID = 2;
                if (requirementEvaluation != null)
                    await _evaluationRepository.UpdateState(requirementEvaluation.evaluationId, EDITION_EVALUATION_STATE_ID);

                if (previous != null && model.maturityLevelId != null && model.maturityLevelId != previous.maturityLevelId)
                {
                    await createBreachFromRequirementEvaluationUpdate(previous.evaluationId, previous.standardId,
                        previous.requirementId, model, previous.companyId);
                }

                return model;
            }
            catch (Exception)
            {
             return BaseApplication.getExceptionErrorResponse();
            }
        }

        private async Task createBreachFromRequirementEvaluationUpdate(int evaluationId, int standardId, int requirementId,
            UpdateRequirementEvaluationDto model, int companyId)
        {
            int? severity = await _maturityLevelBreachGenerator.ResolveBreachSeverityIdAsync(model.maturityLevelId);
            if (severity == null)
                return;

            var existingBreach = await _maturityLevelBreachGenerator.GetOpenBreachAsync(
                evaluationId, MaturityLevelBreachGenerator.BREACH_TYPE_REQUIREMENT, controlId: 0, requirementId);

            if (existingBreach != null)
            {
                await _maturityLevelBreachGenerator.SyncOpenBreachSeverityAsync(existingBreach, severity.Value);
                return;
            }

            var requirements = await (from requirement in _databaseService.Requirement
                                      where ((requirement.isDeleted == null || requirement.isDeleted == false)
                                      && requirement.standardId == standardId
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

            var requirement2 = requirements.Where(r => r.requirementId == requirementId).FirstOrDefault();

            var numerationToShow = "";
            if (requirement2 != null)
                numerationToShow = requirement2.numerationToShow;

            var title = requirement2 != null
                ? $"El requisito {requirement2.numerationToShow} no se cumple: {requirement2.name}"
                : $"El requisito {requirementId} no se cumple";

            var breach = new BreachEntity
            {
                evaluationId = evaluationId,
                standardId = standardId,
                requirementId = requirementId,
                controlId = 0,
                type = MaturityLevelBreachGenerator.BREACH_TYPE_REQUIREMENT,
                title = title,
                description = model.justification ?? "El requisito no cumple con el nivel de madurez esperado.",
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

        private async Task<Notification> existsValidationAsync(int id)
        {
            Notification notification = new Notification();
            int total = await _databaseService.RequirementEvaluation.CountAsync(item => item.requirementEvaluationId == id);
            if (total == 0)
                notification.addError("El Id " + id.ToString() + " no se encuentra registrado");
            return notification;
        }

        private Notification updateValidation(UpdateRequirementEvaluationDto request)
        {
            Notification notification = new Notification();
            request.requiredFieldsValidation(notification);
            return notification;
        }

    }
}

