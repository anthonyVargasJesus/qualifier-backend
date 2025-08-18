using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;
using Qualifier.Domain.Interfaces;
namespace Qualifier.Application.Database.ReferenceDocumentation.Commands.UpdateReferenceDocumentation
{
    public class UpdateReferenceDocumentationCommand : IUpdateReferenceDocumentationCommand
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        private readonly IReferenceDocumentationRepository _referenceDocumentationRepository;

        public UpdateReferenceDocumentationCommand(IDatabaseService databaseService, IMapper mapper, IReferenceDocumentationRepository referenceDocumentationRepository)
        {
            _databaseService = databaseService;
            _mapper = mapper;
            _referenceDocumentationRepository = referenceDocumentationRepository;
        }

        public async Task<Object> Execute(UpdateReferenceDocumentationDto model, int id)
        {
            try
            {
                Notification notification = this.updateValidation(model);
                if (notification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(notification.errors);

                Notification existsNotification = await this.existsValidationAsync(id);
                if (existsNotification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(existsNotification.errors);

                await _referenceDocumentationRepository.Update(id, _mapper.Map<ReferenceDocumentationEntity>(model));

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
            int total = await _databaseService.ReferenceDocumentation.CountAsync(item => item.referenceDocumentationId == id);
            if (total == 0)
                notification.addError("El Id " + id.ToString() + " no se encuentra registrado");
            return notification;
        }

        private Notification updateValidation(UpdateReferenceDocumentationDto request)
        {
            Notification notification = new Notification();
            request.requiredFieldsValidation(notification);
            return notification;
        }

    }
}

