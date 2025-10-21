using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

using Qualifier.Domain.Interfaces;
namespace Qualifier.Application.Database.Macroprocess.Commands.UpdateMacroprocess
{
    public class UpdateMacroprocessCommand : IUpdateMacroprocessCommand
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        private readonly IMacroprocessRepository _macroprocessRepository;

        public UpdateMacroprocessCommand(IDatabaseService databaseService, IMapper mapper, IMacroprocessRepository macroprocessRepository)
        {
            _databaseService = databaseService;
            _mapper = mapper;
            _macroprocessRepository = macroprocessRepository;
        }

        public async Task<Object> Execute(UpdateMacroprocessDto model, int id)
        {
            try
            {
                Notification notification = this.updateValidation(model);
                if (notification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(notification.errors);

                Notification existsNotification = await this.existsValidationAsync(id);
                if (existsNotification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(existsNotification.errors);

                await _macroprocessRepository.Update(id, _mapper.Map<MacroprocessEntity>(model));

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
            int total = await _databaseService.Macroprocess.CountAsync(item => item.macroprocessId == id);
            if (total == 0)
                notification.addError("El Id " + id.ToString() + " no se encuentra registrado");
            return notification;
        }

        private Notification updateValidation(UpdateMacroprocessDto request)
        {
            Notification notification = new Notification();
            request.requiredFieldsValidation(notification);
            return notification;
        }

    }
}

