using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

using Qualifier.Domain.Interfaces;
namespace Qualifier.Application.Database.ResidualRisk.Commands.UpdateResidualRisk
{
    public class UpdateResidualRiskCommand : IUpdateResidualRiskCommand
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        private readonly IResidualRiskRepository _residualRiskRepository;

        public UpdateResidualRiskCommand(IDatabaseService databaseService, IMapper mapper, IResidualRiskRepository residualRiskRepository)
        {
            _databaseService = databaseService;
            _mapper = mapper;
            _residualRiskRepository = residualRiskRepository;
        }

        public async Task<Object> Execute(UpdateResidualRiskDto model, int id)
        {
            try
            {
                Notification notification = this.updateValidation(model);
                if (notification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(notification.errors);

                Notification existsNotification = await this.existsValidationAsync(id);
                if (existsNotification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(existsNotification.errors);

                await _residualRiskRepository.Update(id, _mapper.Map<ResidualRiskEntity>(model));

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
            int total = await _databaseService.ResidualRisk.CountAsync(item => item.residualRiskId == id);
            if (total == 0)
                notification.addError("El Id " + id.ToString() + " no se encuentra registrado");
            return notification;
        }

        private Notification updateValidation(UpdateResidualRiskDto request)
        {
            Notification notification = new Notification();
            request.requiredFieldsValidation(notification);
            return notification;
        }

    }
}

