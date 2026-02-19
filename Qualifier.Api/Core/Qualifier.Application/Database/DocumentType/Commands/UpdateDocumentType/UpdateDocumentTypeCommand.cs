using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

using Qualifier.Domain.Interfaces;
namespace Qualifier.Application.Database.DocumentType.Commands.UpdateDocumentType
{
    public class UpdateDocumentTypeCommand : IUpdateDocumentTypeCommand
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        private readonly IDocumentTypeRepository _documentTypeRepository;

        public UpdateDocumentTypeCommand(IDatabaseService databaseService, IMapper mapper, IDocumentTypeRepository documentTypeRepository)
        {
            _databaseService = databaseService;
            _mapper = mapper;
            _documentTypeRepository = documentTypeRepository;
        }

        public async Task<Object> Execute(UpdateDocumentTypeDto model, int id)
        {
            try
            {
                Notification notification = this.updateValidation(model);
                if (notification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(notification.errors);

                Notification existsNotification = await this.existsValidationAsync(id);
                if (existsNotification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(existsNotification.errors);

                await _documentTypeRepository.Update(id, _mapper.Map<DocumentTypeEntity>(model));

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
            int total = await _databaseService.DocumentType.CountAsync(item => item.documentTypeId == id);
            if (total == 0)
                notification.addError("El Id " + id.ToString() + " no se encuentra registrado");
            return notification;
        }

        private Notification updateValidation(UpdateDocumentTypeDto request)
        {
            Notification notification = new Notification();
            request.requiredFieldsValidation(notification);
            return notification;
        }

    }
}

