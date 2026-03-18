using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Api.Core.Qualifier.Application.Database.User.Commands.UpdateUserImage;
using Qualifier.Application.Database;
using Qualifier.Application.Database.User.Commands.UpdateUser;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;
using Qualifier.Domain.Interfaces;

namespace Qualifier.Api.Core.Qualifier.Application.Database.User.Commands.UpdateImage
{
    public class UpdateUserImageCommand : IUpdateUserImageCommand
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UpdateUserImageCommand(IDatabaseService databaseService, IMapper mapper, IUserRepository userRepository)
        {
            _databaseService = databaseService;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<Object> Execute(int id, UpdateUserImageDto user)
        {
            try
            {
                Notification notification = this.updateValidation(user);
                if (notification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(notification.errors);

                Notification existsNotification = await this.existsValidationAsync(id);
                if (existsNotification.hasErrors())
                    return BaseApplication.getApplicationErrorResponse(existsNotification.errors);

                await _userRepository.UpdateImage(id, user.image);

                return true;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

        private async Task<Notification> existsValidationAsync(int id)
        {
            Notification notification = new Notification();
            int total = await _databaseService.User.CountAsync(item => item.userId == id);
            if (total == 0)
                notification.addError("El Id " + id.ToString() + " no se encuentra registrado");
            return notification;
        }

        private Notification updateValidation(UpdateUserImageDto request)
        {
            Notification notification = new Notification();
            request.requiredFieldsValidation(notification);
            return notification;
        }

    }
}
