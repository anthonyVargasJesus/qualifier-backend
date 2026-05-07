using AutoMapper;
using Qualifier.Application.Database.ActionPlanPriority.Queries.GetActionPlanPriorityById;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.ActionPlanPriority.Commands.CreateActionPlanPriority

{
    public class CreateActionPlanPriorityCommand : ICreateActionPlanPriorityCommand
    {

        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public CreateActionPlanPriorityCommand(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }

        public async Task<Object> Execute(CreateActionPlanPriorityDto model)
        {
            try
            {
                Notification notification = this.createValidation(model);
                if (notification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(notification.errors);

                var entity = _mapper.Map<ActionPlanPriorityEntity>(model);
                entity.creationDate = DateTime.UtcNow;
                entity.creationUserId = model.creationUserId;
                await _databaseService.ActionPlanPriority.AddAsync(entity);
                await _databaseService.SaveAsync();

                return _mapper.Map<GetActionPlanPriorityByIdDto>(entity);
     
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

        private Notification createValidation(CreateActionPlanPriorityDto request)
        {
            Notification notification = new Notification();
            request.requiredFieldsValidation(notification);
            return notification;
        }

    }
}

