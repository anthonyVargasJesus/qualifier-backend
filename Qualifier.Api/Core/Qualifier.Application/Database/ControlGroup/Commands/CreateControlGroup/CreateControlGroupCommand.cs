using AutoMapper;
using Qualifier.Application.Cache;
using Qualifier.Application.Database.ControlEvaluation.Queries.GetControlEvaluationByProcess;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.ControlGroup.Commands.CreateControlGroup

{
    public class CreateControlGroupCommand : ICreateControlGroupCommand
    {

        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        private readonly IAppCacheService _cacheService;

        public CreateControlGroupCommand(IDatabaseService databaseService, IMapper mapper, IAppCacheService cacheService)
        {
            _databaseService = databaseService;
            _mapper = mapper;
            _cacheService = cacheService;
        }

        public async Task<Object> Execute(CreateControlGroupDto model)
        {
            try
            {
                Notification notification = this.createValidation(model);
                if (notification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(notification.errors);

                var entity = _mapper.Map<ControlGroupEntity>(model);
                entity.creationDate = DateTime.UtcNow;
                entity.creationUserId = model.creationUserId;
                await _databaseService.ControlGroup.AddAsync(entity);
                await _databaseService.SaveAsync();
                _cacheService.Remove(GetControlEvaluationByProcessQuery.GroupCacheKey(model.standardId));
                return model;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

        private Notification createValidation(CreateControlGroupDto request)
        {
            Notification notification = new Notification();
            request.requiredFieldsValidation(notification);
            return notification;
        }

    }
}

