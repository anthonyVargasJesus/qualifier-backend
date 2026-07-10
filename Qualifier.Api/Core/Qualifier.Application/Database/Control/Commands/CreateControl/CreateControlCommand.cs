using AutoMapper;
using Qualifier.Application.Cache;
using Qualifier.Application.Database.ControlEvaluation.Queries.GetControlEvaluationByProcess;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.Control.Commands.CreateControl

{
    public class CreateControlCommand : ICreateControlCommand
    {

        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        private readonly IAppCacheService _cacheService;

        public CreateControlCommand(IDatabaseService databaseService, IMapper mapper, IAppCacheService cacheService)
        {
            _databaseService = databaseService;
            _mapper = mapper;
            _cacheService = cacheService;
        }

        public async Task<Object> Execute(CreateControlDto model)
        {
            try
            {
                Notification notification = this.createValidation(model);
                if (notification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(notification.errors);

                var entity = _mapper.Map<ControlEntity>(model);
                entity.creationDate = DateTime.UtcNow;
                entity.creationUserId = model.creationUserId;
                await _databaseService.Control.AddAsync(entity);
                await _databaseService.SaveAsync();
                if (model.standardId != null)
                    _cacheService.Remove(GetControlEvaluationByProcessQuery.ControlCacheKey(model.standardId.Value));
                return model;
            }
            catch (Exception)
            {
             return BaseApplication.getExceptionErrorResponse();
            }
        }

        private Notification createValidation(CreateControlDto request)
        {
            Notification notification = new Notification();
            request.requiredFieldsValidation(notification);
            return notification;
        }

    }
}

