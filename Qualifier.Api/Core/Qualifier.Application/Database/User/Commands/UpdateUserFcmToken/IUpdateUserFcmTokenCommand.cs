namespace Qualifier.Application.Database.User.Commands.UpdateUserFcmToken
{
    public interface IUpdateUserFcmTokenCommand
    {
        Task<Object> Execute(int userId, string fcmToken);
    }
}
