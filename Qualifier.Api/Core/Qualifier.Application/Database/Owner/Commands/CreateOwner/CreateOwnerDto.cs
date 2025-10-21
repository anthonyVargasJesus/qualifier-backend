using Qualifier.Common.Application.NotificationPattern;

namespace Qualifier.Application.Database.Owner.Commands.CreateOwner
{
    public class CreateOwnerDto
    {
        public string code { get; set; }
        public string name { get; set; }
        public int companyId { get; set; }
        public int? creationUserId { get; set; }

        public void requiredFieldsValidation(Notification notification)
        {
            if (name == null)
                notification.addError("El name es obligatorio");

        }

    }
}

