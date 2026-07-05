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

        public CreateRequirementEvaluationCommand(IDatabaseService databaseService, IMapper mapper, 
            IEvaluationRepository evaluationRepository)
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
                    // El cliente decide POST vs PUT según lo que tenía cargado en memoria; si ese
                    // estado quedó desincronizado (reinicio de la app, dos guardados casi
                    // simultáneos, etc.) y ya existe una fila real para este requirement+evaluationId,
                    // se actualiza esa fila en vez de insertar un duplicado (antes esto creaba
                    // MAE_REQUIREMENT_EVALUATION duplicados para el mismo requisito).
                    var entity = await _databaseService.RequirementEvaluation
                        .Where(x => x.requirementId == model.requirementId && x.evaluationId == model.evaluationId
                            && (x.isDeleted == null || x.isDeleted == false))
                        .FirstOrDefaultAsync();

                    bool isNew = entity == null;
                    if (isNew)
                    {
                        entity = _mapper.Map<RequirementEvaluationEntity>(model);
                        const int PENDING_AUDITOR_STATUS_VALUE = 1;
                        entity.auditorStatus = PENDING_AUDITOR_STATUS_VALUE;
                        entity.creationDate = DateTime.UtcNow;
                        entity.creationUserId = model.creationUserId;
                        await _databaseService.RequirementEvaluation.AddAsync(entity);
                    }
                    else
                    {
                        entity.maturityLevelId = model.maturityLevelId ?? entity.maturityLevelId;
                        entity.value = model.value ?? entity.value;
                        entity.responsibleId = model.responsibleId ?? entity.responsibleId;
                        entity.justification = model.justification ?? entity.justification;
                        entity.improvementActions = model.improvementActions ?? entity.improvementActions;
                        entity.updateDate = DateTime.UtcNow;
                        entity.updateUserId = model.creationUserId;
                    }
                    await _databaseService.SaveAsync();
                    long requirementEvaluationId = entity.requirementEvaluationId;
                    // Sin esto, el frontend recibe de vuelta el mismo id=0 que mandó y nunca se
                    // entera de que ya existe una fila real; el próximo guardado volvería a crear
                    // otra en vez de actualizar la que se acaba de insertar.
                    model.requirementEvaluationId = requirementEvaluationId;

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

                    await createBreachFromEvaluation(entity, model);

                    scope.Complete();
                }

                return model;
            }
            catch (Exception)
            {
             return BaseApplication.getExceptionErrorResponse();
            }
        }

        private Notification createValidation(CreateRequirementEvaluationDto request)
        {
            Notification notification = new Notification();
            request.requiredFieldsValidation(notification);


            return notification;
        }

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

            // Por nombre, no por id: el catálogo de maturity levels es editable por el usuario y
            // sus ids pueden cambiar (ej. al reordenar/recrear niveles); el nombre es lo estable.
            var maturityLevel = await _databaseService.MaturityLevel
                .Where(m => m.maturityLevelId == entity.maturityLevelId)
                .FirstOrDefaultAsync();

            int? severity = maturityLevel?.name switch
            {
                "Parcial" => SEVERITY_MEDIUM,
                "No cumple" => SEVERITY_HIGH,
                _ => null,
            };

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

