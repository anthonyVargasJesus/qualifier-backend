using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Application.Cache;
using Qualifier.Application.Database.ControlEvaluation.Queries.GetControlEvaluationByProcess;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;
using Qualifier.Domain.Interfaces;

namespace Qualifier.Application.Database.ControlGroup.Commands.UpdateControlGroup
{
    public class UpdateControlGroupCommand : IUpdateControlGroupCommand
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        private readonly IControlGroupRepository _controlGroupRepository;
        private readonly IAppCacheService _cacheService;

        public UpdateControlGroupCommand(IDatabaseService databaseService, IMapper mapper, IControlGroupRepository controlGroupRepository, IAppCacheService cacheService)
        {
            _databaseService = databaseService;
            _mapper = mapper;
            _controlGroupRepository = controlGroupRepository;
            _cacheService = cacheService;
        }

        public async Task<Object> Execute(UpdateControlGroupDto model, int id)
        {
            try
            {
                Notification notification = this.updateValidation(model);
                if (notification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(notification.errors);

                Notification existsNotification = await this.existsValidationAsync(id);
                if (existsNotification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(existsNotification.errors);

                await _controlGroupRepository.Update(id, _mapper.Map<ControlGroupEntity>(model));

                var standardId = await _databaseService.ControlGroup
                    .Where(item => item.controlGroupId == id)
                    .Select(item => item.standardId)
                    .FirstOrDefaultAsync();
                if (standardId != null)
                    _cacheService.Remove(GetControlEvaluationByProcessQuery.GroupCacheKey(standardId.Value));

                return model;
            }
            catch (Exception )
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

        private Notification updateValidation(UpdateControlGroupDto request)
        {
            Notification notification = new Notification();
            request.requiredFieldsValidation(notification);
            return notification;
        }

    }
}

