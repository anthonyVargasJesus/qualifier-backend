using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

using Qualifier.Domain.Interfaces;
namespace Qualifier.Application.Database.ActiveType.Commands.UpdateActiveType
{
    public class UpdateActiveTypeCommand : IUpdateActiveTypeCommand
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        private readonly IActiveTypeRepository _activeTypeRepository;

        public UpdateActiveTypeCommand(IDatabaseService databaseService, IMapper mapper, IActiveTypeRepository activeTypeRepository)
        {
            _databaseService = databaseService;
            _mapper = mapper;
            _activeTypeRepository = activeTypeRepository;
        }

        public async Task<Object> Execute(UpdateActiveTypeDto model, int id)
        {
            try
            {
                Notification notification = this.updateValidation(model);
                if (notification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(notification.errors);

                Notification existsNotification = await this.existsValidationAsync(id);
                if (existsNotification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(existsNotification.errors);

                await _activeTypeRepository.Update(id, _mapper.Map<ActiveTypeEntity>(model));

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
            int total = await _databaseService.ActiveType.CountAsync(item => item.activeTypeId == id);
            if (total == 0)
                notification.addError("El Id " + id.ToString() + " no se encuentra registrado");
            return notification;
        }
        private Notification updateValidation(UpdateActiveTypeDto request)
        {
            Notification notification = new Notification();
            request.requiredFieldsValidation(notification);
            return notification;
        }

    }
}

