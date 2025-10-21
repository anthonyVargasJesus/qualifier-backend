using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

using Qualifier.Domain.Interfaces;
namespace Qualifier.Application.Database.RequirementInDefaultRisk.Commands.UpdateRequirementInDefaultRisk
{
    public class UpdateRequirementInDefaultRiskCommand : IUpdateRequirementInDefaultRiskCommand
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        private readonly IRequirementInDefaultRiskRepository _requirementInDefaultRiskRepository;

        public UpdateRequirementInDefaultRiskCommand(IDatabaseService databaseService, IMapper mapper, IRequirementInDefaultRiskRepository requirementInDefaultRiskRepository)
        {
            _databaseService = databaseService;
            _mapper = mapper;
            _requirementInDefaultRiskRepository = requirementInDefaultRiskRepository;
        }

        public async Task<Object> Execute(UpdateRequirementInDefaultRiskDto model, int id)
        {
            try
            {
                Notification notification = this.updateValidation(model);
                if (notification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(notification.errors);

                Notification existsNotification = await this.existsValidationAsync(id);
                if (existsNotification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(existsNotification.errors);

                await _requirementInDefaultRiskRepository.Update(id, _mapper.Map<RequirementInDefaultRiskEntity>(model));

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
            int total = await _databaseService.RequirementInDefaultRisk.CountAsync(item => item.requirementInDefaultRiskId == id);
            if (total == 0)
                notification.addError("El Id " + id.ToString() + " no se encuentra registrado");
            return notification;
        }

        private Notification updateValidation(UpdateRequirementInDefaultRiskDto request)
        {
            Notification notification = new Notification();
            request.requiredFieldsValidation(notification);
            return notification;
        }

    }
}

