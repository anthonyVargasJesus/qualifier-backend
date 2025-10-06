using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

using Qualifier.Domain.Interfaces;
namespace Qualifier.Application.Database.ActionPlanPriority.Commands.UpdateActionPlanPriority
{
    public class UpdateActionPlanPriorityCommand : IUpdateActionPlanPriorityCommand
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        private readonly IActionPlanPriorityRepository _actionPlanPriorityRepository;

        public UpdateActionPlanPriorityCommand(IDatabaseService databaseService, IMapper mapper, IActionPlanPriorityRepository actionPlanPriorityRepository)
        {
            _databaseService = databaseService;
            _mapper = mapper;
            _actionPlanPriorityRepository = actionPlanPriorityRepository;
        }

        public async Task<Object> Execute(UpdateActionPlanPriorityDto model, int id)
        {
            try
            {
                Notification notification = this.updateValidation(model);
                if (notification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(notification.errors);

                Notification existsNotification = await this.existsValidationAsync(id);
                if (existsNotification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(existsNotification.errors);

                await _actionPlanPriorityRepository.Update(id, _mapper.Map<ActionPlanPriorityEntity>(model));

                return model;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

        private async Task<Notification> existsValidationAsync(int id)
        {
            Notification notification = new Notification();
            int total = await _databaseService.ActionPlanPriority.CountAsync(item => item.actionPlanPriorityId == id);
            if (total == 0)
                notification.addError("El Id " + id.ToString() + " no se encuentra registrado");
            return notification;
        }

        private Notification updateValidation(UpdateActionPlanPriorityDto request)
        {
            Notification notification = new Notification();
            request.requiredFieldsValidation(notification);
            return notification;
        }

    }
}

