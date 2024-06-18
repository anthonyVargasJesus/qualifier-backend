using Qualifier.Common.Application.NotificationPattern;

namespace Qualifier.Application.Database.User.Commands.CreateUser
{
    public class CreateUserDto
    {
        public int userId { get; set; }
        public string name { get; set; }
        public string? middleName { get; set; }
        public string firstName { get; set; }
        public string? lastName { get; set; }
        public string email { get; set; }
        public string? phone { get; set; }
        public string? password { get; set; }
        public int userStateId { get; set; }
        public string documentNumber { get; set; }
        public int? companyId { get; set; }
        public int? creationUserId { get; set; }

        public void requiredFieldsValidation(Notification notification)
        {
            if (name == null)
                notification.addError("El name es obligatorio");

            if (firstName == null)
                notification.addError("El firstName es obligatorio");

            if (email == null)
                notification.addError("El email es obligatorio");

            if (userStateId == null)
                notification.addError("El userStateId es obligatorio");

            if (documentNumber == null)
                notification.addError("El documentNumber es obligatorio");

        }

    }
}

