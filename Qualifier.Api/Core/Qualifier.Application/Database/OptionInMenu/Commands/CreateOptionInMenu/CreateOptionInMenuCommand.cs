using AutoMapper;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.OptionInMenu.Commands.CreateOptionInMenu

{
    public class CreateOptionInMenuCommand : ICreateOptionInMenuCommand
    {

        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public CreateOptionInMenuCommand(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }

        public async Task<Object> Execute(CreateOptionInMenuDto model)
        {
            try
            {
                Notification notification = this.createValidation(model);
                if (notification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(notification.errors);

                var entity = _mapper.Map<OptionInMenuEntity>(model);
                entity.creationDate = DateTime.UtcNow;
                entity.creationUserId = model.creationUserId;
                await _databaseService.OptionInMenu.AddAsync(entity);
                await _databaseService.SaveAsync();
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
              //return BaseApplication.getExceptionErrorResponse();
            }
        }

        private Notification createValidation(CreateOptionInMenuDto request)
        {
            Notification notification = new Notification();
            request.requiredFieldsValidation(notification);
            return notification;
        }

    }
}

