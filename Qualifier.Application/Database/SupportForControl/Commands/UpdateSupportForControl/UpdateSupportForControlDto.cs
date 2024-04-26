using Qualifier.Common.Application.NotificationPattern;

namespace Qualifier.Application.Database.SupportForControl.Commands.UpdateSupportForControl
{
    public class UpdateSupportForControlDto
    {
        public int supportForControlId { get; set; }
        public int documentationId { get; set; }
        public int controlId { get; set; }
        public int standardId { get; set; }
        public int? updateUserId { get; set; }

        public void requiredFieldsValidation(Notification notification)
        {
            if (documentationId == null)
                notification.addError("El documentationId es obligatorio");

            if (controlId == null)
                notification.addError("El controlId es obligatorio");

        }

    }
}

