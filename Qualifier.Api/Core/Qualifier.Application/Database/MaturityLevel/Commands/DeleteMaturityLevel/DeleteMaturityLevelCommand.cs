using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Application.Cache;
using Qualifier.Application.Database.MaturityLevel.Queries.GetAllMaturityLevelsByCompanyId;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;
using Qualifier.Domain.Interfaces;


namespace Qualifier.Application.Database.MaturityLevel.Commands.DeleteMaturityLevel
{
    public class DeleteMaturityLevelCommand : IDeleteMaturityLevelCommand
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMaturityLevelRepository _repository;
        private readonly IAppCacheService _cacheService;

        public DeleteMaturityLevelCommand(IDatabaseService databaseService, IMaturityLevelRepository repository, IAppCacheService cacheService)
        {
            _databaseService = databaseService;
            _repository = repository;
            _cacheService = cacheService;
        }

        public async Task<Object> Execute(int id, int updateUserId)
        {
            try
            {
                Notification existsNotification = await this.existsValidationAsync(id);
                if (existsNotification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(existsNotification.errors);

                var companyId = await _databaseService.MaturityLevel
                    .Where(item => item.maturityLevelId == id)
                    .Select(item => item.companyId)
                    .FirstOrDefaultAsync();

                await _repository.Delete(id, updateUserId);

                _cacheService.Remove(GetAllMaturityLevelsByCompanyIdQuery.CacheKey(companyId));

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
            var entity = await _databaseService.MaturityLevel.FirstOrDefaultAsync(item => item.maturityLevelId == id);
            if (entity == null)
                notification.addError("El Id " + id.ToString() + " no se encuentra registrado");
            return notification;
        }

    }
}
