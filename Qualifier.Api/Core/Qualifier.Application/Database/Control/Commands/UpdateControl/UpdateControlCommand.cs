using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Application.Cache;
using Qualifier.Application.Database.ControlEvaluation.Queries.GetControlEvaluationByProcess;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;
using Qualifier.Domain.Interfaces;

namespace Qualifier.Application.Database.Control.Commands.UpdateControl
{
    public class UpdateControlCommand : IUpdateControlCommand
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        private readonly IControlRepository _controlRepository;
        private readonly IAppCacheService _cacheService;

        public UpdateControlCommand(IDatabaseService databaseService, IMapper mapper, IControlRepository controlRepository, IAppCacheService cacheService)
        {
            _databaseService = databaseService;
            _mapper = mapper;
            _controlRepository = controlRepository;
            _cacheService = cacheService;
        }

        public async Task<Object> Execute(UpdateControlDto model, int id)
        {
            try
            {
                Notification notification = this.updateValidation(model);
                if (notification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(notification.errors);

                Notification existsNotification = await this.existsValidationAsync(id);
                if (existsNotification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(existsNotification.errors);

                await _controlRepository.Update(id, _mapper.Map<ControlEntity>(model));

                var standardId = await _databaseService.Control
                    .Where(item => item.controlId == id)
                    .Select(item => item.standardId)
                    .FirstOrDefaultAsync();
                _cacheService.Remove(GetControlEvaluationByProcessQuery.ControlCacheKey(standardId));

                return model;
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

        private Notification updateValidation(UpdateControlDto request)
        {
            Notification notification = new Notification();
            request.requiredFieldsValidation(notification);
            return notification;
        }

    }
}

