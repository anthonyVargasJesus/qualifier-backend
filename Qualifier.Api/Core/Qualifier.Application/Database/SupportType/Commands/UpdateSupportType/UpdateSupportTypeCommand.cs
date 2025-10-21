using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

using Qualifier.Domain.Interfaces;
namespace Qualifier.Application.Database.SupportType.Commands.UpdateSupportType
{
    public class UpdateSupportTypeCommand : IUpdateSupportTypeCommand
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        private readonly ISupportTypeRepository _supportTypeRepository;

        public UpdateSupportTypeCommand(IDatabaseService databaseService, IMapper mapper, ISupportTypeRepository supportTypeRepository)
        {
            _databaseService = databaseService;
            _mapper = mapper;
            _supportTypeRepository = supportTypeRepository;
        }

        public async Task<Object> Execute(UpdateSupportTypeDto model, int id)
        {
            try
            {
                Notification notification = this.updateValidation(model);
                if (notification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(notification.errors);

                Notification existsNotification = await this.existsValidationAsync(id);
                if (existsNotification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(existsNotification.errors);

                await _supportTypeRepository.Update(id, _mapper.Map<SupportTypeEntity>(model));

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
            int total = await _databaseService.SupportType.CountAsync(item => item.supportTypeId == id);
            if (total == 0)
                notification.addError("El Id " + id.ToString() + " no se encuentra registrado");
            return notification;
        }

        private Notification updateValidation(UpdateSupportTypeDto request)
        {
            Notification notification = new Notification();
            request.requiredFieldsValidation(notification);
            return notification;
        }

    }
}

