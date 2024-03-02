using Qualifier.Common.Application.NotificationPattern;

namespace Qualifier.Application.Database.Standard.Commands.CreateStandard
{
    public class CreateStandardDto
    {
        public int standardId { get; set; }
        public string? name { get; set; }
        public string? description { get; set; }
        public int parentId { get; set; }
        public int companyId { get; set; }
        public int? creationUserId { get; set; }
        public void requiredFieldsValidation(Notification notification)
        {

            if (name == null)
                notification.addError("El name es obligatorio");

            if (description == null)
                notification.addError("El description es obligatorio");

        }

    }
}

