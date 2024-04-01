using AutoMapper;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.Documentation.Commands.CreateDocumentation

{
    public class CreateDocumentationCommand : ICreateDocumentationCommand
    {

        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public CreateDocumentationCommand(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }

        public async Task<Object> Execute(CreateDocumentationDto model)
        {
            try
            {
                Notification notification = this.createValidation(model);
                if (notification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(notification.errors);

                var entity = _mapper.Map<DocumentationEntity>(model);
                entity.creationDate = DateTime.UtcNow;
                entity.creationUserId = model.creationUserId;
                await _databaseService.Documentation.AddAsync(entity);
                await _databaseService.SaveAsync();
                return model;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

        private Notification createValidation(CreateDocumentationDto request)
        {
            Notification notification = new Notification();
            request.requiredFieldsValidation(notification);
            return notification;
        }

    }
}

