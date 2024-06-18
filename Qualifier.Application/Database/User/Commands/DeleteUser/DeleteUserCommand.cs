using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Interfaces;

namespace Qualifier.Application.Database.User.Commands.DeleteUser
{
    public class DeleteUserCommand : IDeleteUserCommand
    {
        private readonly IDatabaseService _databaseService;
        private readonly IUserRepository _userRepository;

        public DeleteUserCommand(IDatabaseService databaseService, IUserRepository userRepository)
        {
            _databaseService = databaseService;
            _userRepository = userRepository;
        }

        public async Task<Object> Execute(int id, int updateUserId)
        {
            try
            {
                Notification existsNotification = await this.existsValidationAsync(id);
                if (existsNotification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(existsNotification.errors);

                await _userRepository.Delete(id, updateUserId);

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
            int total = await _databaseService.User.CountAsync(item => item.userId == id);
            if (total == 0)
                notification.addError("El Id " + id.ToString() + " no se encuentra registrado");
            return notification;
        }

    }
}

