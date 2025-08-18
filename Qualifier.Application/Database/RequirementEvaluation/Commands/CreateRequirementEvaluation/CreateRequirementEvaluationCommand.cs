using AutoMapper;
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

    }
}

