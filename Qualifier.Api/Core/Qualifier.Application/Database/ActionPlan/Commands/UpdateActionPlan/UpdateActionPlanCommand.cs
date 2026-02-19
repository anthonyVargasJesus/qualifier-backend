using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

using Qualifier.Domain.Interfaces;
namespace Qualifier.Application.Database.ActionPlan.Commands.UpdateActionPlan
{
    public class UpdateActionPlanCommand : IUpdateActionPlanCommand
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        private readonly IActionPlanRepository _actionPlanRepository;

        public UpdateActionPlanCommand(IDatabaseService databaseService, IMapper mapper, IActionPlanRepository actionPlanRepository)
        {
            _databaseService = databaseService;
            _mapper = mapper;
            _actionPlanRepository = actionPlanRepository;
        }

        public async Task<Object> Execute(UpdateActionPlanDto model, int id)
        {
            try
            {
                Notification notification = this.updateValidation(model);
                if (notification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(notification.errors);

                Notification existsNotification = await this.existsValidationAsync(id);
                if (existsNotification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(existsNotification.errors);

                await _actionPlanRepository.Update(id, _mapper.Map<ActionPlanEntity>(model));

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
            int total = await _databaseService.ActionPlan.CountAsync(item => item.actionPlanId == id);
            if (total == 0)
                notification.addError("El Id " + id.ToString() + " no se encuentra registrado");
            return notification;
        }

        private Notification updateValidation(UpdateActionPlanDto request)
        {
            Notification notification = new Notification();
            request.requiredFieldsValidation(notification);
            return notification;
        }

    }
}

