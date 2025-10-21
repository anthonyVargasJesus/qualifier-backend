using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

using Qualifier.Domain.Interfaces;
namespace Qualifier.Application.Database.ValuationInActive.Commands.UpdateValuationInActive
{
    public class UpdateValuationInActiveCommand : IUpdateValuationInActiveCommand
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        private readonly IValuationInActiveRepository _valuationInActiveRepository;

        public UpdateValuationInActiveCommand(IDatabaseService databaseService, IMapper mapper, IValuationInActiveRepository valuationInActiveRepository)
        {
            _databaseService = databaseService;
            _mapper = mapper;
            _valuationInActiveRepository = valuationInActiveRepository;
        }

        public async Task<Object> Execute(UpdateValuationInActiveDto model, int id)
        {
            try
            {
                Notification notification = this.updateValidation(model);
                if (notification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(notification.errors);

                Notification existsNotification = await this.existsValidationAsync(id);
                if (existsNotification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(existsNotification.errors);

                await _valuationInActiveRepository.Update(id, _mapper.Map<ValuationInActiveEntity>(model));

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
            int total = await _databaseService.ValuationInActive.CountAsync(item => item.valuationInActiveId == id);
            if (total == 0)
                notification.addError("El Id " + id.ToString() + " no se encuentra registrado");
            return notification;
        }

        private Notification updateValidation(UpdateValuationInActiveDto request)
        {
            Notification notification = new Notification();
            request.requiredFieldsValidation(notification);
            return notification;
        }

    }
}

