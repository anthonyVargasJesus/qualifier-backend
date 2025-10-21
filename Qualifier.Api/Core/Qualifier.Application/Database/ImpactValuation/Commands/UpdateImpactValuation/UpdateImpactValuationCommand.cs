using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

using Qualifier.Domain.Interfaces;
namespace Qualifier.Application.Database.ImpactValuation.Commands.UpdateImpactValuation
{
    public class UpdateImpactValuationCommand : IUpdateImpactValuationCommand
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        private readonly IImpactValuationRepository _impactValuationRepository;

        public UpdateImpactValuationCommand(IDatabaseService databaseService, IMapper mapper, IImpactValuationRepository impactValuationRepository)
        {
            _databaseService = databaseService;
            _mapper = mapper;
            _impactValuationRepository = impactValuationRepository;
        }

        public async Task<Object> Execute(UpdateImpactValuationDto model, int id)
        {
            try
            {
                Notification notification = this.updateValidation(model);
                if (notification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(notification.errors);

                Notification existsNotification = await this.existsValidationAsync(id);
                if (existsNotification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(existsNotification.errors);

                await _impactValuationRepository.Update(id, _mapper.Map<ImpactValuationEntity>(model));

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
            int total = await _databaseService.ImpactValuation.CountAsync(item => item.impactValuationId == id);
            if (total == 0)
                notification.addError("El Id " + id.ToString() + " no se encuentra registrado");
            return notification;
        }

        private Notification updateValidation(UpdateImpactValuationDto request)
        {
            Notification notification = new Notification();
            request.requiredFieldsValidation(notification);
            return notification;
        }

    }
}

