using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

using Qualifier.Domain.Interfaces;
namespace Qualifier.Application.Database.MenaceType.Commands.UpdateMenaceType
{
    public class UpdateMenaceTypeCommand : IUpdateMenaceTypeCommand
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        private readonly IMenaceTypeRepository _menaceTypeRepository;

        public UpdateMenaceTypeCommand(IDatabaseService databaseService, IMapper mapper, IMenaceTypeRepository menaceTypeRepository)
        {
            _databaseService = databaseService;
            _mapper = mapper;
            _menaceTypeRepository = menaceTypeRepository;
        }

        public async Task<Object> Execute(UpdateMenaceTypeDto model, int id)
        {
            try
            {
                Notification notification = this.updateValidation(model);
                if (notification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(notification.errors);

                Notification existsNotification = await this.existsValidationAsync(id);
                if (existsNotification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(existsNotification.errors);

                await _menaceTypeRepository.Update(id, _mapper.Map<MenaceTypeEntity>(model));

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
            int total = await _databaseService.MenaceType.CountAsync(item => item.menaceTypeId == id);
            if (total == 0)
                notification.addError("El Id " + id.ToString() + " no se encuentra registrado");
            return notification;
        }

        private Notification updateValidation(UpdateMenaceTypeDto request)
        {
            Notification notification = new Notification();
            request.requiredFieldsValidation(notification);
            return notification;
        }

    }
}

