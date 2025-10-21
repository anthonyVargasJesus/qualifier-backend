using Qualifier.Common.Application.NotificationPattern;

namespace Qualifier.Application.Database.DocumentType.Commands.UpdateDocumentType
{
    public class UpdateDocumentTypeDto
    {
        public int documentTypeId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int? updateUserId { get; set; }

        public void requiredFieldsValidation(Notification notification)
        {

            if (name == null)
                notification.addError("El name es obligatorio");

        }

    }
}

