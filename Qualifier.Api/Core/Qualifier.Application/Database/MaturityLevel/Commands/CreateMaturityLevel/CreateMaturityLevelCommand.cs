using AutoMapper;
using Qualifier.Application.Cache;
using Qualifier.Application.Database.MaturityLevel.Queries.GetAllMaturityLevelsByCompanyId;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;


namespace Qualifier.Application.Database.MaturityLevel.Commands.CreateMaturityLevel
{
    public class CreateMaturityLevelCommand : ICreateMaturityLevelCommand
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        private readonly IAppCacheService _cacheService;

        public CreateMaturityLevelCommand(IDatabaseService databaseService, IMapper mapper, IAppCacheService cacheService)
        {
            _databaseService = databaseService;
            _mapper = mapper;
            _cacheService = cacheService;
        }
        public async Task<Object> Execute(CreateMaturityLevelDto model)
        {
            try
            {
                Notification notification = this.createValidation(model);
                if (notification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(notification.errors);

                var entity = _mapper.Map<MaturityLevelEntity>(model);
                await _databaseService.MaturityLevel.AddAsync(entity);
                await _databaseService.SaveAsync();
                _cacheService.Remove(GetAllMaturityLevelsByCompanyIdQuery.CacheKey(model.companyId));
                return model;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

        private Notification createValidation(CreateMaturityLevelDto request)
        {
            Notification notification = new Notification();
            request.requiredFieldsValidation(notification);
            return notification;
        }

    }
}
