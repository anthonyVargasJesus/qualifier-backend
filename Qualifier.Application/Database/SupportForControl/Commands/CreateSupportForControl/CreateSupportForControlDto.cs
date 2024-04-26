using Qualifier.Common.Application.NotificationPattern;

namespace Qualifier.Application.Database.SupportForControl.Commands.CreateSupportForControl
{
    public class CreateSupportForControlDto
    {
        public int supportForControlId { get; set; }
        public int documentationId { get; set; }
        public int controlId { get; set; }
        public int standardId { get; set; }
        public int companyId { get; set; }
        public int? creationUserId { get; set; }

        public void requiredFieldsValidation(Notification notification)
        {
            if (documentationId == null)
                notification.addError("El documentationId es obligatorio");

            if (controlId == null)
                notification.addError("El controlId es obligatorio");

        }

    }
}

