using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

using Qualifier.Domain.Interfaces;
namespace Qualifier.Application.Database.Responsible.Commands.UpdateResponsible
{
    public class UpdateResponsibleCommand : IUpdateResponsibleCommand
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        private readonly IResponsibleRepository _responsibleRepository;

        public UpdateResponsibleCommand(IDatabaseService databaseService, IMapper mapper, IResponsibleRepository responsibleRepository)
        {
            _databaseService = databaseService;
            _mapper = mapper;
            _responsibleRepository = responsibleRepository;
        }

        public async Task<Object> Execute(UpdateResponsibleDto model, int id)
        {
            try
            {
                Notification notification = this.updateValidation(model);
                if (notification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(notification.errors);

                Notification existsNotification = await this.existsValidationAsync(id);
                if (existsNotification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(existsNotification.errors);

                await _responsibleRepository.Update(id, _mapper.Map<ResponsibleEntity>(model));

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
            int total = await _databaseService.Responsible.CountAsync(item => item.responsibleId == id);
            if (total == 0)
                notification.addError("El Id " + id.ToString() + " no se encuentra registrado");
            return notification;
        }

        private Notification updateValidation(UpdateResponsibleDto request)
        {
            Notification notification = new Notification();
            request.requiredFieldsValidation(notification);
            return notification;
        }

    }
}

