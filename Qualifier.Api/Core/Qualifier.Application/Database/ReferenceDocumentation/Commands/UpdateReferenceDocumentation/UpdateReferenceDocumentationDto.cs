using Qualifier.Common.Application.NotificationPattern;

namespace Qualifier.Application.Database.ReferenceDocumentation.Commands.UpdateReferenceDocumentation
{
    public class UpdateReferenceDocumentationDto
    {
        public int referenceDocumentationId { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public string? description { get; set; }
        public int documentationId { get; set; }
        public int? updateUserId { get; set; }

        public void requiredFieldsValidation(Notification notification)
        {
            if (name == null)
                notification.addError("El name es obligatorio");

            if (url == null)
                notification.addError("El url es obligatorio");

            if (documentationId == null)
                notification.addError("El documentationId es obligatorio");

        }

    }
}

