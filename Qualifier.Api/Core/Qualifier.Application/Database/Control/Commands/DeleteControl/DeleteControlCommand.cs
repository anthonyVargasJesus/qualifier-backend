using Microsoft.EntityFrameworkCore;
using Qualifier.Application.Cache;
using Qualifier.Application.Database.ControlEvaluation.Queries.GetControlEvaluationByProcess;
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
        private readonly IAppCacheService _cacheService;

        public DeleteControlCommand(IDatabaseService databaseService, IControlRepository controlRepository, IAppCacheService cacheService)
        {
            _databaseService = databaseService;
            _controlRepository = controlRepository;
            _cacheService = cacheService;
        }

        public async Task<Object> Execute(int id, int updateUserId)
        {
            try
            {
                Notification existsNotification = await this.existsValidationAsync(id);
                if (existsNotification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(existsNotification.errors);

                var standardId = await _databaseService.Control
                    .Where(item => item.controlId == id)
                    .Select(item => item.standardId)
                    .FirstOrDefaultAsync();

                await _controlRepository.Delete(id, updateUserId);

                _cacheService.Remove(GetControlEvaluationByProcessQuery.ControlCacheKey(standardId));

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

