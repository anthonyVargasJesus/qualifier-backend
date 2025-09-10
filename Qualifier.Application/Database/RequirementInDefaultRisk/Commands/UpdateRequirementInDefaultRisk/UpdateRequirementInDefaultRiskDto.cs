using Qualifier.Common.Application.NotificationPattern;

namespace Qualifier.Application.Database.RequirementInDefaultRisk.Commands.UpdateRequirementInDefaultRisk
{
    public class UpdateRequirementInDefaultRiskDto
    {
        public int requirementInDefaultRiskId { get; set; }
        public int defaultRiskId { get; set; }
        public int requirementId { get; set; }
        public bool? isActive { get; set; }
        public int? updateUserId { get; set; }

        public void requiredFieldsValidation(Notification notification)
        {
            if (defaultRiskId == null)
                notification.addError("El defaultRiskId es obligatorio");

            if (requirementId == null)
                notification.addError("El requirementId es obligatorio");

        }

    }
}

