using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

using Qualifier.Domain.Interfaces;
namespace Qualifier.Application.Database.ControlInDefaultRisk.Commands.UpdateControlInDefaultRisk
{
    public class UpdateControlInDefaultRiskCommand : IUpdateControlInDefaultRiskCommand
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        private readonly IControlInDefaultRiskRepository _controlInDefaultRiskRepository;

        public UpdateControlInDefaultRiskCommand(IDatabaseService databaseService, IMapper mapper, IControlInDefaultRiskRepository controlInDefaultRiskRepository)
        {
            _databaseService = databaseService;
            _mapper = mapper;
            _controlInDefaultRiskRepository = controlInDefaultRiskRepository;
        }

        public async Task<Object> Execute(UpdateControlInDefaultRiskDto model, int id)
        {
            try
            {
                Notification notification = this.updateValidation(model);
                if (notification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(notification.errors);

                Notification existsNotification = await this.existsValidationAsync(id);
                if (existsNotification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(existsNotification.errors);

                await _controlInDefaultRiskRepository.Update(id, _mapper.Map<ControlInDefaultRiskEntity>(model));

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
            int total = await _databaseService.ControlInDefaultRisk.CountAsync(item => item.controlInDefaultRiskId == id);
            if (total == 0)
                notification.addError("El Id " + id.ToString() + " no se encuentra registrado");
            return notification;
        }

        private Notification updateValidation(UpdateControlInDefaultRiskDto request)
        {
            Notification notification = new Notification();
            request.requiredFieldsValidation(notification);
            return notification;
        }

    }
}

