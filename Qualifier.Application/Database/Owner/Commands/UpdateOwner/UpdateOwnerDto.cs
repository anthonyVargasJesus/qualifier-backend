using Qualifier.Common.Application.NotificationPattern;

namespace Qualifier.Application.Database.Owner.Commands.UpdateOwner
{
    public class UpdateOwnerDto
    {
        public int ownerId { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public int? updateUserId { get; set; }

        public void requiredFieldsValidation(Notification notification)
        {
            if (name == null)
                notification.addError("El name es obligatorio");

        }

    }
}

