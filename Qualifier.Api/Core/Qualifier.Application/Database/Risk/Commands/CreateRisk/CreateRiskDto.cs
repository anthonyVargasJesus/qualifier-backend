using Qualifier.Common.Application.NotificationPattern;

namespace Qualifier.Application.Database.Risk.Commands.CreateRisk
{
    public class CreateRiskDto
    {
        public int riskId { get; set; }
        public string? name { get; set; }
        public int evaluationId { get; set; }
        public int activesInventoryId { get; set; }
        public string? activesInventoryNumber { get; set; }
        public string? activesInventoryName { get; set; }
        public int menaceId { get; set; }
        public int vulnerabilityId { get; set; }
        public int breachId { get; set; }
        public int? companyId { get; set; }
        public int? creationUserId { get; set; }

        public void requiredFieldsValidation(Notification notification)
        {
            if (evaluationId == null)
                notification.addError("El evaluationId es obligatorio");

            if (activesInventoryId == null)
                notification.addError("El activesInventoryId es obligatorio");

            if (menaceId == null)
                notification.addError("El menaceId es obligatorio");

            if (vulnerabilityId == null)
                notification.addError("El vulnerabilityId es obligatorio");

        }

    }
}

