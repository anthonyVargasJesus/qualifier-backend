using Qualifier.Common.Application.NotificationPattern;

namespace Qualifier.Application.Database.ActivesInventoryInDefaultRisk.Commands.CreateActivesInventoryInDefaultRisk
{
    public class CreateActivesInventoryInDefaultRiskDto
    {
        public int? activesInventoryInDefaultRiskId { get; set; }
        public int? defaultRiskId { get; set; }
        public int? activesInventoryId { get; set; }
        public bool? isActive { get; set; }
        public int? companyId { get; set; }
        public int? creationUserId { get; set; }

        public void requiredFieldsValidation(Notification notification)
        {
            if (defaultRiskId == null && defaultRiskId <= 0)
                notification.addError("El defaultRiskId es obligatorio");

            if (activesInventoryId == null && defaultRiskId <= 0)
                notification.addError("El activesInventoryId es obligatorio");

        }

    }
}

