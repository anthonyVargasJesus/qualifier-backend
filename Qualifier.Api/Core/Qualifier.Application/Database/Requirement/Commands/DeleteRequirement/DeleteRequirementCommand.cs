using Microsoft.EntityFrameworkCore;
using Qualifier.Application.Cache;
using Qualifier.Application.Database.RequirementEvaluation.Queries.GetRequirementEvaluationByProcess;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Interfaces;
namespace Qualifier.Application.Database.Requirement.Commands.DeleteRequirement
{
    public class DeleteRequirementCommand : IDeleteRequirementCommand
    {
        private readonly IDatabaseService _databaseService;
        private readonly IRequirementRepository _requirementRepository;
        private readonly IAppCacheService _cacheService;

        public DeleteRequirementCommand(IDatabaseService databaseService, IRequirementRepository requirementRepository, IAppCacheService cacheService)
        {
            _databaseService = databaseService;
            _requirementRepository = requirementRepository;
            _cacheService = cacheService;
        }

        public async Task<Object> Execute(int id, int updateUserId)
        {
            try
            {
                Notification existsNotification = await this.existsValidationAsync(id);
                if (existsNotification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(existsNotification.errors);

                var standardId = await _databaseService.Requirement
                    .Where(item => item.requirementId == id)
                    .Select(item => item.standardId)
                    .FirstOrDefaultAsync();

                await _requirementRepository.Delete(id, updateUserId);

                _cacheService.Remove(GetRequirementEvaluationByProcessQuery.CacheKey(standardId));

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
            int total = await _databaseService.Requirement.CountAsync(item => item.requirementId == id);
            if (total == 0)
                notification.addError("El Id " + id.ToString() + " no se encuentra registrado");
            return notification;
        }

    }
}

