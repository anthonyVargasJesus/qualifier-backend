using Qualifier.Common.Application.NotificationPattern;

namespace Qualifier.Application.Database.UserState.Commands.UpdateUserState
{
    public class UpdateUserStateDto
    {
        public int userStateId { get; set; }
        public string name { get; set; }
        public int value { get; set; }
        public int? updateUserId { get; set; }

        public void requiredFieldsValidation(Notification notification)
        {
            if (name == null)
                notification.addError("El name es obligatorio");

            if (value == null)
                notification.addError("El value es obligatorio");

        }

    }
}

