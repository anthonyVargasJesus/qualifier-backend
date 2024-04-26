using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

using Qualifier.Domain.Interfaces;
namespace Qualifier.Application.Database.ConfidentialityLevel.Commands.UpdateConfidentialityLevel
{
    public class UpdateConfidentialityLevelCommand : IUpdateConfidentialityLevelCommand
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        private readonly IConfidentialityLevelRepository _confidentialityLevelRepository;

        public UpdateConfidentialityLevelCommand(IDatabaseService databaseService, IMapper mapper, IConfidentialityLevelRepository confidentialityLevelRepository)
        {
            _databaseService = databaseService;
            _mapper = mapper;
            _confidentialityLevelRepository = confidentialityLevelRepository;
        }

        public async Task<Object> Execute(UpdateConfidentialityLevelDto model, int id)
        {
            try
            {
                Notification notification = this.updateValidation(model);
                if (notification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(notification.errors);

                Notification existsNotification = await this.existsValidationAsync(id);
                if (existsNotification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(existsNotification.errors);

                await _confidentialityLevelRepository.Update(id, _mapper.Map<ConfidentialityLevelEntity>(model));

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
            int total = await _databaseService.ConfidentialityLevel.CountAsync(item => item.confidentialityLevelId == id);
            if (total == 0)
                notification.addError("El Id " + id.ToString() + " no se encuentra registrado");
            return notification;
        }

        private Notification updateValidation(UpdateConfidentialityLevelDto request)
        {
            Notification notification = new Notification();
            request.requiredFieldsValidation(notification);
            return notification;
        }

    }
}

