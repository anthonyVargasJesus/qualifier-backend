using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Interfaces;

namespace Qualifier.Application.Database.UsageClassification.Commands.DeleteUsageClassification
{
    public class DeleteUsageClassificationCommand : IDeleteUsageClassificationCommand
    {
        private readonly IDatabaseService _databaseService;
        private readonly IUsageClassificationRepository _usageClassificationRepository;

        public DeleteUsageClassificationCommand(IDatabaseService databaseService, IUsageClassificationRepository usageClassificationRepository)
        {
            _databaseService = databaseService;
            _usageClassificationRepository = usageClassificationRepository;
        }

        public async Task<Object> Execute(int id, int updateUserId)
        {
            try
            {
                Notification existsNotification = await this.existsValidationAsync(id);
                if (existsNotification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(existsNotification.errors);

                await _usageClassificationRepository.Delete(id, updateUserId);

                BaseResponseCommandDto baseResponseCommandDto = new BaseResponseCommandDto();
                baseResponseCommandDto.response = "¡Registro eliminado!";
                return baseResponseCommandDto;

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

    }
}

