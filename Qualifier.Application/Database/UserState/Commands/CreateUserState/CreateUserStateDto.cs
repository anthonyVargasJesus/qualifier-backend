using Qualifier.Common.Application.NotificationPattern;

namespace Qualifier.Application.Database.UserState.Commands.CreateUserState
{
    public class CreateUserStateDto
    {
        public int userStateId { get; set; }
        public string name { get; set; }
        public int value { get; set; }
        public int? companyId { get; set; }
        public int? creationUserId { get; set; }

        public void requiredFieldsValidation(Notification notification)
        {
            if (name == null)
                notification.addError("El name es obligatorio");

            if (value == null)
                notification.addError("El value es obligatorio");

        }

    }
}

