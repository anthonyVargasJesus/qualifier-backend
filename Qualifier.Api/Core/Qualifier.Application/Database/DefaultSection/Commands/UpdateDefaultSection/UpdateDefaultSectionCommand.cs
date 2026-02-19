using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

using Qualifier.Domain.Interfaces;
namespace Qualifier.Application.Database.DefaultSection.Commands.UpdateDefaultSection
{
    public class UpdateDefaultSectionCommand : IUpdateDefaultSectionCommand
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        private readonly IDefaultSectionRepository _defaultSectionRepository;

        public UpdateDefaultSectionCommand(IDatabaseService databaseService, IMapper mapper, IDefaultSectionRepository defaultSectionRepository)
        {
            _databaseService = databaseService;
            _mapper = mapper;
            _defaultSectionRepository = defaultSectionRepository;
        }

        public async Task<Object> Execute(UpdateDefaultSectionDto model, int id)
        {
            try
            {
                Notification notification = this.updateValidation(model);
                if (notification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(notification.errors);

                Notification existsNotification = await this.existsValidationAsync(id);
                if (existsNotification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(existsNotification.errors);

                await _defaultSectionRepository.Update(id, _mapper.Map<DefaultSectionEntity>(model));

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
            int total = await _databaseService.DefaultSection.CountAsync(item => item.defaultSectionId == id);
            if (total == 0)
                notification.addError("El Id " + id.ToString() + " no se encuentra registrado");
            return notification;
        }

        private Notification updateValidation(UpdateDefaultSectionDto request)
        {
            Notification notification = new Notification();
            request.requiredFieldsValidation(notification);
            return notification;
        }

    }
}

