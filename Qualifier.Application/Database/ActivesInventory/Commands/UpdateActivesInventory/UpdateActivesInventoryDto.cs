using Qualifier.Common.Application.NotificationPattern;

namespace Qualifier.Application.Database.ActivesInventory.Commands.UpdateActivesInventory
{
    public class UpdateActivesInventoryDto
    {
        public int activesInventoryId { get; set; }
        public string number { get; set; }
        public int macroprocessId { get; set; }
        public int subprocessId { get; set; }
        public string? procedure { get; set; }
        public int activeTypeId { get; set; }
        public string name { get; set; }
        public string? description { get; set; }
        public int ownerId { get; set; }
        public int custodianId { get; set; }
        public int usageClassificationId { get; set; }
        public int supportTypeId { get; set; }
        public int locationId { get; set; }
        public int? updateUserId { get; set; }

        public void requiredFieldsValidation(Notification notification)
        {
            if (number == null)
                notification.addError("El number es obligatorio");

            if (macroprocessId == null)
                notification.addError("El macroprocessId es obligatorio");

            if (subprocessId == null)
                notification.addError("El subprocessId es obligatorio");

            if (activeTypeId == null)
                notification.addError("El activeTypeId es obligatorio");

            if (name == null)
                notification.addError("El name es obligatorio");

            if (ownerId == null)
                notification.addError("El ownerId es obligatorio");

            if (custodianId == null)
                notification.addError("El custodianId es obligatorio");

            if (usageClassificationId == null)
                notification.addError("El usageClassificationId es obligatorio");

            if (supportTypeId == null)
                notification.addError("El supportTypeId es obligatorio");

            if (locationId == null)
                notification.addError("El locationId es obligatorio");

        }

    }
}

