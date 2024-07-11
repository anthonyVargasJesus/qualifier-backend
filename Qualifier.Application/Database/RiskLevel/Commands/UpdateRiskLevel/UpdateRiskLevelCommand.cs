using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

using Qualifier.Domain.Interfaces;
namespace Qualifier.Application.Database.RiskLevel.Commands.UpdateRiskLevel
{
    public class UpdateRiskLevelCommand : IUpdateRiskLevelCommand
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        private readonly IRiskLevelRepository _riskLevelRepository;

        public UpdateRiskLevelCommand(IDatabaseService databaseService, IMapper mapper, IRiskLevelRepository riskLevelRepository)
        {
            _databaseService = databaseService;
            _mapper = mapper;
            _riskLevelRepository = riskLevelRepository;
        }

        public async Task<Object> Execute(UpdateRiskLevelDto model, int id)
        {
            try
            {
                Notification notification = this.updateValidation(model);
                if (notification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(notification.errors);

                Notification existsNotification = await this.existsValidationAsync(id);
                if (existsNotification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(existsNotification.errors);

                await _riskLevelRepository.Update(id, _mapper.Map<RiskLevelEntity>(model));

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
            int total = await _databaseService.RiskLevel.CountAsync(item => item.riskLevelId == id);
            if (total == 0)
                notification.addError("El Id " + id.ToString() + " no se encuentra registrado");
            return notification;
        }

        private Notification updateValidation(UpdateRiskLevelDto request)
        {
            Notification notification = new Notification();
            request.requiredFieldsValidation(notification);
            return notification;
        }

    }
}

