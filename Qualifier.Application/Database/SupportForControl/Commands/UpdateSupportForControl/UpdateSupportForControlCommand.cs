using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

using Qualifier.Domain.Interfaces;
namespace Qualifier.Application.Database.SupportForControl.Commands.UpdateSupportForControl
{
    public class UpdateSupportForControlCommand : IUpdateSupportForControlCommand
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        private readonly ISupportForControlRepository _supportForControlRepository;

        public UpdateSupportForControlCommand(IDatabaseService databaseService, IMapper mapper, ISupportForControlRepository supportForControlRepository)
        {
            _databaseService = databaseService;
            _mapper = mapper;
            _supportForControlRepository = supportForControlRepository;
        }

        public async Task<Object> Execute(UpdateSupportForControlDto model, int id)
        {
            try
            {
                Notification notification = this.updateValidation(model);
                if (notification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(notification.errors);

                Notification existsNotification = await this.existsValidationAsync(id);
                if (existsNotification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(existsNotification.errors);

                await _supportForControlRepository.Update(id, _mapper.Map<SupportForControlEntity>(model));

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
            int total = await _databaseService.SupportForControl.CountAsync(item => item.supportForControlId == id);
            if (total == 0)
                notification.addError("El Id " + id.ToString() + " no se encuentra registrado");
            return notification;
        }

        private Notification updateValidation(UpdateSupportForControlDto request)
        {
            Notification notification = new Notification();
            request.requiredFieldsValidation(notification);
            return notification;
        }

    }
}

