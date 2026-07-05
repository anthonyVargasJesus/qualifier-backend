using Qualifier.Common.Application.Service;
using Qualifier.Domain.Interfaces;

namespace Qualifier.Application.Database.User.Commands.UpdateUserFcmToken
{
    public class UpdateUserFcmTokenCommand : IUpdateUserFcmTokenCommand
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserFcmTokenCommand(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Object> Execute(int userId, string fcmToken)
        {
            try
            {
                await _userRepository.UpdateFcmToken(userId, fcmToken);
                return true;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }
    }
}
