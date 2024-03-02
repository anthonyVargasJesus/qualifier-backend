using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;
using Qualifier.Domain.Interfaces;


namespace Qualifier.Application.Database.MaturityLevel.Commands.UpdateMaturityLevel
{
    public class UpdateMaturityLevelCommand : IUpdateMaturityLevelCommand
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        private readonly IMaturityLevelRepository _repository;

        public UpdateMaturityLevelCommand(IDatabaseService databaseService, IMapper mapper, IMaturityLevelRepository repository)
        {
            _databaseService = databaseService;
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<Object> Execute(UpdateMaturityLevelDto model, int id)
        {
            try
            {
                Notification notification = this.updateValidation(model);
                if (notification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(notification.errors);

                Notification existsNotification = await this.existsValidationAsync(id);
                if (existsNotification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(existsNotification.errors);

                await _repository.Update(id, _mapper.Map<MaturityLevelEntity>(model));

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
            int total = await _databaseService.MaturityLevel.CountAsync(item => item.maturityLevelId == id);
            if (total == 0)
                notification.addError("El Id " + id.ToString() + " no se encuentra registrado");
            return notification;
        }

        private Notification updateValidation(UpdateMaturityLevelDto request)
        {
            Notification notification = new Notification();
            request.requiredFieldsValidation(notification);
            return notification;
        }

    }
}
