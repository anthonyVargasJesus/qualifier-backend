using Qualifier.Common.Application.NotificationPattern;

namespace Qualifier.Application.Database.Documentation.Commands.UpdateDocumentation
{
    public class UpdateDocumentationDto
    {
        public int documentationId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string template { get; set; }
        public int? documentTypeId { get; set; }
        public int? updateUserId { get; set; }

        public void requiredFieldsValidation(Notification notification)
        {
            if (name == null)
                notification.addError("El name es obligatorio");

        }

    }
}

