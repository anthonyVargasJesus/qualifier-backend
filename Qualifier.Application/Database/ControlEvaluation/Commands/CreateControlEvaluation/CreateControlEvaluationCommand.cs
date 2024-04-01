using AutoMapper;
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

    }
}

