using Qualifier.Common.Application.NotificationPattern;

namespace Qualifier.Application.Database.ActivesInventory.Commands.CreateActivesInventory
{
    public class CreateActivesInventoryDto
    {
        public int? activesInventoryId { get; set; }
        public string? number { get; set; }
        public int? macroprocessId { get; set; }
        public int? subprocessId { get; set; }
        public string? procedure { get; set; }
        public int? activeTypeId { get; set; }
        public string? name { get; set; }
        public string? description { get; set; }
        public int? ownerId { get; set; }
        public int? custodianId { get; set; }
        public int? usageClassificationId { get; set; }
        public int? supportTypeId { get; set; }
        public int? locationId { get; set; }
        public int? standardId { get; set; }
        public int? companyId { get; set; }
        public int? creationUserId { get; set; }

        public void requiredFieldsValidation(Notification notification)
        {
            if (number == null)
                notification.addError("El number es obligatorio");

            if (macroprocessId == null && macroprocessId <= 0)
                notification.addError("El macroprocessId es obligatorio");

            if (subprocessId == null && subprocessId <= 0)
                notification.addError("El subprocessId es obligatorio");

            if (activeTypeId == null && activeTypeId <= 0)
                notification.addError("El activeTypeId es obligatorio");

            if (name == null)
                notification.addError("El name es obligatorio");

            if (ownerId == null && ownerId <= 0)
                notification.addError("El ownerId es obligatorio");

            if (custodianId == null && custodianId <= 0)
                notification.addError("El custodianId es obligatorio");

            if (usageClassificationId == null && usageClassificationId <= 0)
                notification.addError("El usageClassificationId es obligatorio");

            if (supportTypeId == null && supportTypeId <= 0)
                notification.addError("El supportTypeId es obligatorio");

            if (locationId == null && locationId <= 0)
                notification.addError("El locationId es obligatorio");

        }

    }
}

