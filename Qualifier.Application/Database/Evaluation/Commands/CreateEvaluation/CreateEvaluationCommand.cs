using AutoMapper;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.Evaluation.Commands.CreateEvaluation

{
    public class CreateEvaluationCommand : ICreateEvaluationCommand
    {

        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public CreateEvaluationCommand(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }

        public async Task<Object> Execute(CreateEvaluationDto model)
        {
            try
            {
                Notification notification = this.createValidation(model);
                if (notification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(notification.errors);

                var entity = _mapper.Map<EvaluationEntity>(model);
                entity.startDate = DateTime.SpecifyKind(entity.startDate, DateTimeKind.Utc);
                entity.endDate = DateTime.SpecifyKind(entity.endDate, DateTimeKind.Utc);
                entity.creationDate = DateTime.UtcNow;
                entity.creationUserId = model.creationUserId;
                await _databaseService.Evaluation.AddAsync(entity);
                await _databaseService.SaveAsync();
                return model;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

        private Notification createValidation(CreateEvaluationDto request)
        {
            Notification notification = new Notification();
            request.requiredFieldsValidation(notification);
            return notification;
        }

    }
}

