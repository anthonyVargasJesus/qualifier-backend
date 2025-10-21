using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

using Qualifier.Domain.Interfaces;
namespace Qualifier.Application.Database.UsageClassification.Commands.UpdateUsageClassification
{
    public class UpdateUsageClassificationCommand : IUpdateUsageClassificationCommand
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        private readonly IUsageClassificationRepository _usageClassificationRepository;

        public UpdateUsageClassificationCommand(IDatabaseService databaseService, IMapper mapper, IUsageClassificationRepository usageClassificationRepository)
        {
            _databaseService = databaseService;
            _mapper = mapper;
            _usageClassificationRepository = usageClassificationRepository;
        }

        public async Task<Object> Execute(UpdateUsageClassificationDto model, int id)
        {
            try
            {
                Notification notification = this.updateValidation(model);
                if (notification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(notification.errors);

                Notification existsNotification = await this.existsValidationAsync(id);
                if (existsNotification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(existsNotification.errors);

                await _usageClassificationRepository.Update(id, _mapper.Map<UsageClassificationEntity>(model));

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
            int total = await _databaseService.UsageClassification.CountAsync(item => item.usageClassificationId == id);
            if (total == 0)
                notification.addError("El Id " + id.ToString() + " no se encuentra registrado");
            return notification;
        }

        private Notification updateValidation(UpdateUsageClassificationDto request)
        {
            Notification notification = new Notification();
            request.requiredFieldsValidation(notification);
            return notification;
        }

    }
}

