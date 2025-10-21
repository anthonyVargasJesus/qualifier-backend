using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

using Qualifier.Domain.Interfaces;
namespace Qualifier.Application.Database.ActivesInventory.Commands.UpdateActivesInventory
{
    public class UpdateActivesInventoryCommand : IUpdateActivesInventoryCommand
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        private readonly IActivesInventoryRepository _activesInventoryRepository;

        public UpdateActivesInventoryCommand(IDatabaseService databaseService, IMapper mapper, IActivesInventoryRepository activesInventoryRepository)
        {
            _databaseService = databaseService;
            _mapper = mapper;
            _activesInventoryRepository = activesInventoryRepository;
        }

        public async Task<Object> Execute(UpdateActivesInventoryDto model, int id)
        {
            try
            {
                Notification notification = this.updateValidation(model);
                if (notification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(notification.errors);

                Notification existsNotification = await this.existsValidationAsync(id);
                if (existsNotification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(existsNotification.errors);

                await _activesInventoryRepository.Update(id, _mapper.Map<ActivesInventoryEntity>(model));

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
            int total = await _databaseService.ActivesInventory.CountAsync(item => item.activesInventoryId == id);
            if (total == 0)
                notification.addError("El Id " + id.ToString() + " no se encuentra registrado");
            return notification;
        }

        private Notification updateValidation(UpdateActivesInventoryDto request)
        {
            Notification notification = new Notification();
            request.requiredFieldsValidation(notification);
            return notification;
        }

    }
}

