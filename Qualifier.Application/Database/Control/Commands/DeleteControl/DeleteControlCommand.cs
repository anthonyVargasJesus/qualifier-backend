using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto

;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Interfaces;
namespace Qualifier.Application.Database.Control.Commands.DeleteControl
{
    public class DeleteControlCommand : IDeleteControlCommand
    {
        private readonly IDatabaseService _databaseService;
        private readonly IControlRepository _controlRepository;

        public DeleteControlCommand(IDatabaseService databaseService, IControlRepository controlRepository)
        {
            _databaseService = databaseService;
            _controlRepository = controlRepository;
        }

        public async Task<Object> Execute(int id, int updateUserId)
        {
            try
            {
                Notification existsNotification = await this.existsValidationAsync(id);
                if (existsNotification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(existsNotification.errors);

                await _controlRepository.Delete(id, updateUserId);

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
            int total = await _databaseService.Control.CountAsync(item => item.controlId == id);
            if (total == 0)
                notification.addError("El Id " + id.ToString() + " no se encuentra registrado");
            return notification;
        }

    }
}

