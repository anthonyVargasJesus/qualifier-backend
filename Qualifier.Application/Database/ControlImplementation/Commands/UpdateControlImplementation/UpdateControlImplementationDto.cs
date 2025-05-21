using Qualifier.Common.Application.NotificationPattern;

namespace Qualifier.Application.Database.ControlImplementation.Commands.UpdateControlImplementation
{
    public class UpdateControlImplementationDto
    {
        public int controlImplementationId { get; set; }
        public string activities { get; set; }
        public DateTime startDate { get; set; }
        public DateTime? verificationDate { get; set; }
        public int responsibleId { get; set; }
        public string? observation { get; set; }
        public int? updateUserId { get; set; }

        public void requiredFieldsValidation(Notification notification)
        {
            if (activities == null)
                notification.addError("El activities es obligatorio");

            if (startDate == null)
                notification.addError("El startDate es obligatorio");

            if (responsibleId == null)
                notification.addError("El responsibleId es obligatorio");

        }

    }
}

