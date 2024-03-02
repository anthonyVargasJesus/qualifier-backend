using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Domain.Helpers;
using Qualifier.Domain.Entities;
using Qualifier.Domain.Interfaces;

namespace pangolin.domain.services
{
    public class LoginService: ILoginService
    {
        public Notification loginValidation(LoginEntity login, string storedHash)
        {
            Notification notification = new Notification();
            validateExists(notification, login);
            validatePassword(notification, login.password, storedHash);
            login.validation(notification);
            return notification;
        }
        public void validateExists(Notification notification, LoginEntity login)
        {
            if (login == null)
            {
                notification.addError("El usuario " + login.email + " no se encuentra registrado");
                return;
            }
        }
        public void validatePassword(Notification notification, string password, string hash)
        {
            if (!Hashing.ValidatePassword(password, hash))
            {
                notification.addError("La contraseña ingresada es incorrecta");
                return;
            }
        }

    }
}
