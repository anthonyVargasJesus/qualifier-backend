using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

using Qualifier.Domain.Interfaces;
namespace Qualifier.Application.Database.Subprocess.Commands.UpdateSubprocess
{
    public class UpdateSubprocessCommand : IUpdateSubprocessCommand
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        private readonly ISubprocessRepository _subprocessRepository;

        public UpdateSubprocessCommand(IDatabaseService databaseService, IMapper mapper, ISubprocessRepository subprocessRepository)
        {
            _databaseService = databaseService;
            _mapper = mapper;
            _subprocessRepository = subprocessRepository;
        }

        public async Task<Object> Execute(UpdateSubprocessDto model, int id)
        {
            try
            {
                Notification notification = this.updateValidation(model);
                if (notification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(notification.errors);

                Notification existsNotification = await this.existsValidationAsync(id);
                if (existsNotification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(existsNotification.errors);

                await _subprocessRepository.Update(id, _mapper.Map<SubprocessEntity>(model));

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
            int total = await _databaseService.Subprocess.CountAsync(item => item.subprocessId == id);
            if (total == 0)
                notification.addError("El Id " + id.ToString() + " no se encuentra registrado");
            return notification;
        }

        private Notification updateValidation(UpdateSubprocessDto request)
        {
            Notification notification = new Notification();
            request.requiredFieldsValidation(notification);
            return notification;
        }

    }
}

