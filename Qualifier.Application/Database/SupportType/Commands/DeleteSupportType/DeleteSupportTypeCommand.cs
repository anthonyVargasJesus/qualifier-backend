using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Interfaces;

namespace Qualifier.Application.Database.SupportType.Commands.DeleteSupportType
{
    public class DeleteSupportTypeCommand : IDeleteSupportTypeCommand
    {
        private readonly IDatabaseService _databaseService;
        private readonly ISupportTypeRepository _supportTypeRepository;

        public DeleteSupportTypeCommand(IDatabaseService databaseService, ISupportTypeRepository supportTypeRepository)
        {
            _databaseService = databaseService;
            _supportTypeRepository = supportTypeRepository;
        }

        public async Task<Object> Execute(int id, int updateUserId)
        {
            try
            {
                Notification existsNotification = await this.existsValidationAsync(id);
                if (existsNotification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(existsNotification.errors);

                await _supportTypeRepository.Delete(id, updateUserId);

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
            int total = await _databaseService.SupportType.CountAsync(item => item.supportTypeId == id);
            if (total == 0)
                notification.addError("El Id " + id.ToString() + " no se encuentra registrado");
            return notification;
        }

    }
}

