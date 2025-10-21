using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

using Qualifier.Domain.Interfaces;
namespace Qualifier.Application.Database.OptionInMenu.Commands.UpdateOptionInMenu
{
    public class UpdateOptionInMenuCommand : IUpdateOptionInMenuCommand
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        private readonly IOptionInMenuRepository _optionInMenuRepository;

        public UpdateOptionInMenuCommand(IDatabaseService databaseService, IMapper mapper, IOptionInMenuRepository optionInMenuRepository)
        {
            _databaseService = databaseService;
            _mapper = mapper;
            _optionInMenuRepository = optionInMenuRepository;
        }

        public async Task<Object> Execute(UpdateOptionInMenuDto model, int id)
        {
            try
            {
                Notification notification = this.updateValidation(model);
                if (notification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(notification.errors);

                Notification existsNotification = await this.existsValidationAsync(id);
                if (existsNotification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(existsNotification.errors);

                await _optionInMenuRepository.Update(id, _mapper.Map<OptionInMenuEntity>(model));

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
            int total = await _databaseService.OptionInMenu.CountAsync(item => item.optionInMenuId == id);
            if (total == 0)
                notification.addError("El Id " + id.ToString() + " no se encuentra registrado");
            return notification;
        }

        private Notification updateValidation(UpdateOptionInMenuDto request)
        {
            Notification notification = new Notification();
            request.requiredFieldsValidation(notification);
            return notification;
        }

    }
}

