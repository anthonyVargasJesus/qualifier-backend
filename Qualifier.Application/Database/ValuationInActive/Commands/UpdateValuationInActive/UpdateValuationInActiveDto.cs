using Qualifier.Common.Application.NotificationPattern;

namespace Qualifier.Application.Database.ValuationInActive.Commands.UpdateValuationInActive
{
    public class UpdateValuationInActiveDto
    {
        public int valuationInActiveId { get; set; }
        public int activesInventoryId { get; set; }
        public int impactValuationId { get; set; }
        public decimal value { get; set; }
        public int? updateUserId { get; set; }

        public void requiredFieldsValidation(Notification notification)
        {
            if (activesInventoryId == null)
                notification.addError("El activesInventoryId es obligatorio");

            if (impactValuationId == null)
                notification.addError("El impactValuationId es obligatorio");

            if (value == null)
                notification.addError("El value es obligatorio");

        }

    }
}

