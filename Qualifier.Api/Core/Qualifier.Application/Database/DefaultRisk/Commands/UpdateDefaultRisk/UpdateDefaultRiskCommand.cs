using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

using Qualifier.Domain.Interfaces;
namespace Qualifier.Application.Database.DefaultRisk.Commands.UpdateDefaultRisk
{
    public class UpdateDefaultRiskCommand : IUpdateDefaultRiskCommand
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        private readonly IDefaultRiskRepository _defaultRiskRepository;

        public UpdateDefaultRiskCommand(IDatabaseService databaseService, IMapper mapper, IDefaultRiskRepository defaultRiskRepository)
        {
            _databaseService = databaseService;
            _mapper = mapper;
            _defaultRiskRepository = defaultRiskRepository;
        }

        public async Task<Object> Execute(UpdateDefaultRiskDto model, int id)
        {
            try
            {
                Notification notification = this.updateValidation(model);
                if (notification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(notification.errors);

                Notification existsNotification = await this.existsValidationAsync(id);
                if (existsNotification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(existsNotification.errors);

                await _defaultRiskRepository.Update(id, _mapper.Map<DefaultRiskEntity>(model));

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
            int total = await _databaseService.DefaultRisk.CountAsync(item => item.defaultRiskId == id);
            if (total == 0)
                notification.addError("El Id " + id.ToString() + " no se encuentra registrado");
            return notification;
        }

        private Notification updateValidation(UpdateDefaultRiskDto request)
        {
            Notification notification = new Notification();
            request.requiredFieldsValidation(notification);
            return notification;
        }

    }
}

