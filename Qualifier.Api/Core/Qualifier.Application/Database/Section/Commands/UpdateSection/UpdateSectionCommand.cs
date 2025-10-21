using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;
using Qualifier.Domain.Interfaces;
namespace Qualifier.Application.Database.Section.Commands.UpdateSection
{
    public class UpdateSectionCommand : IUpdateSectionCommand
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        private readonly ISectionRepository _sectionRepository;

        public UpdateSectionCommand(IDatabaseService databaseService, IMapper mapper, ISectionRepository sectionRepository)
        {
            _databaseService = databaseService;
            _mapper = mapper;
            _sectionRepository = sectionRepository;
        }

        public async Task<Object> Execute(UpdateSectionDto model, int id)
        {
            try
            {
                Notification notification = this.updateValidation(model);
                if (notification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(notification.errors);

                Notification existsNotification = await this.existsValidationAsync(id);
                if (existsNotification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(existsNotification.errors);

                await _sectionRepository.Update(id, _mapper.Map<SectionEntity>(model));

                return model;
            }
            catch (Exception ex)
            {
                throw ex;
                //return BaseApplication.getExceptionErrorResponse();
            }
        }

        private async Task<Notification> existsValidationAsync(int id)
        {
            Notification notification = new Notification();
            int total = await _databaseService.Section.CountAsync(item => item.sectionId == id);
            if (total == 0)
                notification.addError("El Id " + id.ToString() + " no se encuentra registrado");
            return notification;
        }

        private Notification updateValidation(UpdateSectionDto request)
        {
            Notification notification = new Notification();
            request.requiredFieldsValidation(notification);
            return notification;
        }

    }
}

