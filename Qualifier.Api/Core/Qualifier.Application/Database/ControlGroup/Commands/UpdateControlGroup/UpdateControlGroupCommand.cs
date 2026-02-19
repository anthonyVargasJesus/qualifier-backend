using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;
using Qualifier.Domain.Interfaces;

namespace Qualifier.Application.Database.ControlGroup.Commands.UpdateControlGroup
{
    public class UpdateControlGroupCommand : IUpdateControlGroupCommand
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        private readonly IControlGroupRepository _controlGroupRepository;

        public UpdateControlGroupCommand(IDatabaseService databaseService, IMapper mapper, IControlGroupRepository controlGroupRepository)
        {
            _databaseService = databaseService;
            _mapper = mapper;
            _controlGroupRepository = controlGroupRepository;
        }

        public async Task<Object> Execute(UpdateControlGroupDto model, int id)
        {
            try
            {
                Notification notification = this.updateValidation(model);
                if (notification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(notification.errors);

                Notification existsNotification = await this.existsValidationAsync(id);
                if (existsNotification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(existsNotification.errors);

                await _controlGroupRepository.Update(id, _mapper.Map<ControlGroupEntity>(model));

                return model;
            }
            catch (Exception )
            {
             return BaseApplication.getExceptionErrorResponse();
            }
        }

        private async Task<Notification> existsValidationAsync(int id)
        {
            Notification notification = new Notification();
            int total = await _databaseService.ControlGroup.CountAsync(item => item.controlGroupId == id);
            if (total == 0)
                notification.addError("El Id " + id.ToString() + " no se encuentra registrado");
            return notification;
        }

        private Notification updateValidation(UpdateControlGroupDto request)
        {
            Notification notification = new Notification();
            request.requiredFieldsValidation(notification);
            return notification;
        }

    }
}

