using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;
using Qualifier.Domain.Interfaces;

namespace Qualifier.Application.Database.Indicator.Commands.UpdateIndicator
{
    public class UpdateIndicatorCommand : IUpdateIndicatorCommand
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        private readonly IIndicatorRepository _indicatorRepository; 
        public UpdateIndicatorCommand(IDatabaseService databaseService, IMapper mapper, IIndicatorRepository indicatorRepository)
        {
            _databaseService = databaseService;
            _mapper = mapper;
            _indicatorRepository = indicatorRepository;
        }

        public async Task<Object> Execute(UpdateIndicatorDto model, int id)
        {
            try
            {
                Notification notification = this.updateValidation(model);
                if (notification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(notification.errors);

                Notification existsNotification = await this.existsValidationAsync(id);
                if (existsNotification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(existsNotification.errors);

                await _indicatorRepository.Update(id, _mapper.Map<IndicatorEntity>(model));

                return model;
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
            int total = await _databaseService.Indicator.CountAsync(item => item.indicatorId == id);
            if (total == 0)
                notification.addError("El Id " + id.ToString() + " no se encuentra registrado");
            return notification;
        }

        private Notification updateValidation(UpdateIndicatorDto request)
        {
            Notification notification = new Notification();
            request.requiredFieldsValidation(notification);
            return notification;
        }

    }
}

