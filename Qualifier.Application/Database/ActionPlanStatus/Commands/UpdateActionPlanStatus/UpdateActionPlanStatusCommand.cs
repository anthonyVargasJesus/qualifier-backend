using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

using Qualifier.Domain.Interfaces;
namespace Qualifier.Application.Database.ActionPlanStatus.Commands.UpdateActionPlanStatus
{
    public class UpdateActionPlanStatusCommand : IUpdateActionPlanStatusCommand
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        private readonly IActionPlanStatusRepository _actionPlanStatusRepository;

        public UpdateActionPlanStatusCommand(IDatabaseService databaseService, IMapper mapper, IActionPlanStatusRepository actionPlanStatusRepository)
        {
            _databaseService = databaseService;
            _mapper = mapper;
            _actionPlanStatusRepository = actionPlanStatusRepository;
        }

        public async Task<Object> Execute(UpdateActionPlanStatusDto model, int id)
        {
            try
            {
                Notification notification = this.updateValidation(model);
                if (notification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(notification.errors);

                Notification existsNotification = await this.existsValidationAsync(id);
                if (existsNotification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(existsNotification.errors);

                await _actionPlanStatusRepository.Update(id, _mapper.Map<ActionPlanStatusEntity>(model));

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
            int total = await _databaseService.ActionPlanStatus.CountAsync(item => item.actionPlanStatusId == id);
            if (total == 0)
                notification.addError("El Id " + id.ToString() + " no se encuentra registrado");
            return notification;
        }

        private Notification updateValidation(UpdateActionPlanStatusDto request)
        {
            Notification notification = new Notification();
            request.requiredFieldsValidation(notification);
            return notification;
        }

    }
}

