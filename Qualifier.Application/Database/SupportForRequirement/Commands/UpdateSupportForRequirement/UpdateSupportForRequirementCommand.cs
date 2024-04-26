using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

using Qualifier.Domain.Interfaces;
namespace Qualifier.Application.Database.SupportForRequirement.Commands.UpdateSupportForRequirement
{
    public class UpdateSupportForRequirementCommand : IUpdateSupportForRequirementCommand
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        private readonly ISupportForRequirementRepository _supportForRequirementRepository;

        public UpdateSupportForRequirementCommand(IDatabaseService databaseService, IMapper mapper, ISupportForRequirementRepository supportForRequirementRepository)
        {
            _databaseService = databaseService;
            _mapper = mapper;
            _supportForRequirementRepository = supportForRequirementRepository;
        }

        public async Task<Object> Execute(UpdateSupportForRequirementDto model, int id)
        {
            try
            {
                Notification notification = this.updateValidation(model);
                if (notification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(notification.errors);

                Notification existsNotification = await this.existsValidationAsync(id);
                if (existsNotification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(existsNotification.errors);

                await _supportForRequirementRepository.Update(id, _mapper.Map<SupportForRequirementEntity>(model));

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
            int total = await _databaseService.SupportForRequirement.CountAsync(item => item.supportForRequirementId == id);
            if (total == 0)
                notification.addError("El Id " + id.ToString() + " no se encuentra registrado");
            return notification;
        }

        private Notification updateValidation(UpdateSupportForRequirementDto request)
        {
            Notification notification = new Notification();
            request.requiredFieldsValidation(notification);
            return notification;
        }

    }
}

