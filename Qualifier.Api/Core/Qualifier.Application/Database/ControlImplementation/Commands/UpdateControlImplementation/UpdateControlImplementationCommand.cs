using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

using Qualifier.Domain.Interfaces;
namespace Qualifier.Application.Database.ControlImplementation.Commands.UpdateControlImplementation
{
    public class UpdateControlImplementationCommand : IUpdateControlImplementationCommand
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        private readonly IControlImplementationRepository _controlImplementationRepository;

        public UpdateControlImplementationCommand(IDatabaseService databaseService, IMapper mapper, IControlImplementationRepository controlImplementationRepository)
        {
            _databaseService = databaseService;
            _mapper = mapper;
            _controlImplementationRepository = controlImplementationRepository;
        }

        public async Task<Object> Execute(UpdateControlImplementationDto model, int id)
        {
            try
            {
                Notification notification = this.updateValidation(model);
                if (notification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(notification.errors);

                Notification existsNotification = await this.existsValidationAsync(id);
                if (existsNotification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(existsNotification.errors);

                await _controlImplementationRepository.Update(id, _mapper.Map<ControlImplementationEntity>(model));

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
            int total = await _databaseService.ControlImplementation.CountAsync(item => item.controlImplementationId == id);
            if (total == 0)
                notification.addError("El Id " + id.ToString() + " no se encuentra registrado");
            return notification;
        }

        private Notification updateValidation(UpdateControlImplementationDto request)
        {
            Notification notification = new Notification();
            request.requiredFieldsValidation(notification);
            return notification;
        }

    }
}

