using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Domain.Entities;

namespace Qualifier.Domain.Interfaces
{
    public interface ILoginService
    {
        Notification loginValidation(LoginEntity login);
        void validateExists(Notification notification, LoginEntity login);
        void validatePassword(Notification notification, string password, string hash);

    }
}
