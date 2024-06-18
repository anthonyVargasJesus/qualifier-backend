using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Interfaces;

namespace Qualifier.Application.Database.OptionInMenuInRole.Commands.DeleteOptionInMenuInRole
{
    public class DeleteOptionInMenuInRoleCommand : IDeleteOptionInMenuInRoleCommand
    {
        private readonly IDatabaseService _databaseService;
        private readonly IOptionInMenuInRoleRepository _optionInMenuInRoleRepository;

        public DeleteOptionInMenuInRoleCommand(IDatabaseService databaseService, IOptionInMenuInRoleRepository optionInMenuInRoleRepository)
        {
            _databaseService = databaseService;
            _optionInMenuInRoleRepository = optionInMenuInRoleRepository;
        }

        public async Task<Object> Execute(int id, int updateUserId)
        {
            try
            {
                Notification existsNotification = await this.existsValidationAsync(id);
                if (existsNotification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(existsNotification.errors);

                await _optionInMenuInRoleRepository.Delete(id, updateUserId);

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
            int total = await _databaseService.OptionInMenuInRole.CountAsync(item => item.optionInMenuInRoleId == id);
            if (total == 0)
                notification.addError("El Id " + id.ToString() + " no se encuentra registrado");
            return notification;
        }

    }
}

