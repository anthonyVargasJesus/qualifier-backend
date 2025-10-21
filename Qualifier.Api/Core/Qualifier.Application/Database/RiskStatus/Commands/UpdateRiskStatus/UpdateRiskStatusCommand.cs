using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

using Qualifier.Domain.Interfaces;
namespace Qualifier.Application.Database.RiskStatus.Commands.UpdateRiskStatus
{
    public class UpdateRiskStatusCommand : IUpdateRiskStatusCommand
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        private readonly IRiskStatusRepository _riskStatusRepository;

        public UpdateRiskStatusCommand(IDatabaseService databaseService, IMapper mapper, IRiskStatusRepository riskStatusRepository)
        {
            _databaseService = databaseService;
            _mapper = mapper;
            _riskStatusRepository = riskStatusRepository;
        }

        public async Task<Object> Execute(UpdateRiskStatusDto model, int id)
        {
            try
            {
                Notification notification = this.updateValidation(model);
                if (notification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(notification.errors);

                Notification existsNotification = await this.existsValidationAsync(id);
                if (existsNotification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(existsNotification.errors);

                await _riskStatusRepository.Update(id, _mapper.Map<RiskStatusEntity>(model));

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
            int total = await _databaseService.RiskStatus.CountAsync(item => item.riskStatusId == id);
            if (total == 0)
                notification.addError("El Id " + id.ToString() + " no se encuentra registrado");
            return notification;
        }

        private Notification updateValidation(UpdateRiskStatusDto request)
        {
            Notification notification = new Notification();
            request.requiredFieldsValidation(notification);
            return notification;
        }

    }
}

