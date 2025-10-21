using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

using Qualifier.Domain.Interfaces;
namespace Qualifier.Application.Database.RoleInUser.Commands.UpdateRoleInUser
{
    public class UpdateRoleInUserCommand : IUpdateRoleInUserCommand
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        private readonly IRoleInUserRepository _roleInUserRepository;

        public UpdateRoleInUserCommand(IDatabaseService databaseService, IMapper mapper, IRoleInUserRepository roleInUserRepository)
        {
            _databaseService = databaseService;
            _mapper = mapper;
            _roleInUserRepository = roleInUserRepository;
        }

        public async Task<Object> Execute(UpdateRoleInUserDto model, int id)
        {
            try
            {
                Notification notification = this.updateValidation(model);
                if (notification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(notification.errors);

                Notification existsNotification = await this.existsValidationAsync(id);
                if (existsNotification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(existsNotification.errors);

                await _roleInUserRepository.Update(id, _mapper.Map<RoleInUserEntity>(model));

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
            int total = await _databaseService.RoleInUser.CountAsync(item => item.roleInUserId == id);
            if (total == 0)
                notification.addError("El Id " + id.ToString() + " no se encuentra registrado");
            return notification;
        }

        private Notification updateValidation(UpdateRoleInUserDto request)
        {
            Notification notification = new Notification();
            request.requiredFieldsValidation(notification);
            return notification;
        }

    }
}

