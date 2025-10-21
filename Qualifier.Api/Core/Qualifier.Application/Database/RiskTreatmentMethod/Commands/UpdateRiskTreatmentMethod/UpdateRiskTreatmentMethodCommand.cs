using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

using Qualifier.Domain.Interfaces;
namespace Qualifier.Application.Database.RiskTreatmentMethod.Commands.UpdateRiskTreatmentMethod
{
    public class UpdateRiskTreatmentMethodCommand : IUpdateRiskTreatmentMethodCommand
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        private readonly IRiskTreatmentMethodRepository _riskTreatmentMethodRepository;

        public UpdateRiskTreatmentMethodCommand(IDatabaseService databaseService, IMapper mapper, 
            IRiskTreatmentMethodRepository riskTreatmentMethodRepository)
        {
            _databaseService = databaseService;
            _mapper = mapper;
            _riskTreatmentMethodRepository = riskTreatmentMethodRepository;
        }

        public async Task<Object> Execute(UpdateRiskTreatmentMethodDto model, int id)
        {
            try
            {
                Notification notification = this.updateValidation(model);
                if (notification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(notification.errors);

                Notification existsNotification = await this.existsValidationAsync(id);
                if (existsNotification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(existsNotification.errors);

                await _riskTreatmentMethodRepository.Update(id, _mapper.Map<RiskTreatmentMethodEntity>(model));

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
            int total = await _databaseService.RiskTreatmentMethod
                .CountAsync(item => item.riskTreatmentMethodId == id);
            if (total == 0)
                notification.addError("El Id " + id.ToString() + " no se encuentra registrado");
            return notification;
        }

        private Notification updateValidation(UpdateRiskTreatmentMethodDto request)
        {
            Notification notification = new Notification();
            request.requiredFieldsValidation(notification);
            return notification;
        }

    }
}

