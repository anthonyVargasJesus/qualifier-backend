using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Domain.Entities;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.User.Commands.Login
{
    public class LoginUserCommandDto
    {
    }

    public class LoginUserLoginTryDto
    {
        public string? email { get; set; }
        public string? uid { get; set; }
        public string? tokenFirebase { get; set; }

        public void requiredValidation(Notification notification)
        {
            if (email == null || email == "")
                notification.addError("El correo electrónico es obligatorio");

            //if (password == null || password == "")
            //    notification.addError("La contraseña es obligatoria");

        }
    }

    public class LoginUserLoginDto
    {
        public int userId { get; set; }
        public string? name { get; set; }
        public string? email { get; set; }
        public LoginUserUserStateDto? userState { get; set; }
        public string? token { get; set; }
        public List<LoginUserRoleDto>? roles { get; set; }
    }

    public class LoginUserRoleDto
    {
        public int roleId { get; set; }
        public string? code { get; set; }
        public string? name { get; set; }

    }

    public class LoginUserUserStateDto
    {
        public int userStateId { get; set; }
        public string? name { get; set; }
    }



}
