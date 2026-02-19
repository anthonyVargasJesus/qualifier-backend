using AutoMapper;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.Standard.Commands.CreateStandard

{
    public class CreateStandardCommand : ICreateStandardCommand
    {

        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public CreateStandardCommand(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }

        public async Task<Object> Execute(CreateStandardDto model)
        {
            try
            {
                Notification notification = this.createValidation(model);
                if (notification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(notification.errors);

                var entity = _mapper.Map<StandardEntity>(model);
                entity.creationDate = DateTime.UtcNow;
                entity.creationUserId = model.creationUserId;
                entity.companyId = model.companyId;
                await _databaseService.Standard.AddAsync(entity);
                await _databaseService.SaveAsync();
                return model;
            }
            catch (Exception)
            {
             return BaseApplication.getExceptionErrorResponse();
            }
        }

        private Notification createValidation(CreateStandardDto request)
        {
            Notification notification = new Notification();
            request.requiredFieldsValidation(notification);
            return notification;
        }

    }
}

