using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Interfaces;
namespace Qualifier.Application.Database.Standard.Commands.DeleteStandard
{
    public class DeleteStandardCommand : IDeleteStandardCommand
    {
        private readonly IDatabaseService _databaseService;
        private readonly IStandardRepository _standardRepository;

        public DeleteStandardCommand(IDatabaseService databaseService, IStandardRepository standardRepository)
        {
            _databaseService = databaseService;
            _standardRepository = standardRepository;
        }

        public async Task<Object> Execute(int id, int updateUserId)
        {
            try


            {
                Notification existsNotification = await this.existsValidationAsync(id);
                if (existsNotification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(existsNotification.errors);

                await _standardRepository.Delete(id, updateUserId);

                BaseResponseCommandDto baseResponseCommandDto = new BaseResponseCommandDto();
                baseResponseCommandDto.response = "¡Registro eliminado!";
                return baseResponseCommandDto;

            }
            catch (Exception ex)
            {
                throw ex;
              //return BaseApplication.getExceptionErrorResponse();
            }
        }

        private async Task<Notification> existsValidationAsync(int id)
        {
            Notification notification = new Notification();
            int total = await _databaseService.Standard.CountAsync(item => item.standardId == id);
            if (total == 0)
                notification.addError("El Id " + id.ToString() + " no se encuentra registrado");
            return notification;
        }

    }
}

