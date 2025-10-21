using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

using Qualifier.Domain.Interfaces;
namespace Qualifier.Application.Database.RiskTreatment.Commands.UpdateRiskTreatment
{
    public class UpdateRiskTreatmentCommand : IUpdateRiskTreatmentCommand
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        private readonly IRiskTreatmentRepository _riskTreatmentRepository;

        public UpdateRiskTreatmentCommand(IDatabaseService databaseService, IMapper mapper, IRiskTreatmentRepository riskTreatmentRepository)
        {
            _databaseService = databaseService;
            _mapper = mapper;
            _riskTreatmentRepository = riskTreatmentRepository;
        }

        public async Task<Object> Execute(UpdateRiskTreatmentDto model, int id)
        {
            try
            {
                Notification notification = this.updateValidation(model);
                if (notification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(notification.errors);

                Notification existsNotification = await this.existsValidationAsync(id);
                if (existsNotification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(existsNotification.errors);

                await _riskTreatmentRepository.Update(id, _mapper.Map<RiskTreatmentEntity>(model));

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
            int total = await _databaseService.RiskTreatment.CountAsync(item => item.riskTreatmentId == id);
            if (total == 0)
                notification.addError("El Id " + id.ToString() + " no se encuentra registrado");
            return notification;
        }

        private Notification updateValidation(UpdateRiskTreatmentDto request)
        {
            Notification notification = new Notification();
            request.requiredFieldsValidation(notification);
            return notification;
        }

    }
}

