using Microsoft.EntityFrameworkCore;
using Qualifier.Application.Cache;
using Qualifier.Application.Database.ControlEvaluation.Queries.GetControlEvaluationByProcess;
using Qualifier.Common.Application.Dto

;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Interfaces;
namespace Qualifier.Application.Database.ControlGroup.Commands.DeleteControlGroup
{
    public class DeleteControlGroupCommand : IDeleteControlGroupCommand
    {
        private readonly IDatabaseService _databaseService;
        private readonly IControlGroupRepository _controlGroupRepository;
        private readonly IAppCacheService _cacheService;

        public DeleteControlGroupCommand(IDatabaseService databaseService, IControlGroupRepository controlGroupRepository, IAppCacheService cacheService)
        {
            _databaseService = databaseService;
            _controlGroupRepository = controlGroupRepository;
            _cacheService = cacheService;
        }

        public async Task<Object> Execute(int id, int updateUserId)
        {
            try
            {
                Notification existsNotification = await this.existsValidationAsync(id);
                if (existsNotification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(existsNotification.errors);

                var standardId = await _databaseService.ControlGroup
                    .Where(item => item.controlGroupId == id)
                    .Select(item => item.standardId)
                    .FirstOrDefaultAsync();

                await _controlGroupRepository.Delete(id, updateUserId);

                if (standardId != null)
                    _cacheService.Remove(GetControlEvaluationByProcessQuery.GroupCacheKey(standardId.Value));

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
            int total = await _databaseService.ControlGroup.CountAsync(item => item.controlGroupId == id);
            if (total == 0)
                notification.addError("El Id " + id.ToString() + " no se encuentra registrado");
            return notification;
        }

    }
}

