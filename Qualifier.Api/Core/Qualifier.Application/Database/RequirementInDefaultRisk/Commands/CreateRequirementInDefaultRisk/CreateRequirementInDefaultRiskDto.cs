using Qualifier.Common.Application.NotificationPattern;

namespace Qualifier.Application.Database.RequirementInDefaultRisk.Commands.CreateRequirementInDefaultRisk
{
    public class CreateRequirementInDefaultRiskDto
    {
        public int requirementInDefaultRiskId { get; set; }
        public int defaultRiskId { get; set; }
        public int requirementId { get; set; }
        public bool? isActive { get; set; }
        public int? companyId { get; set; }
        public int? creationUserId { get; set; }

        public void requiredFieldsValidation(Notification notification)
        {
            if (defaultRiskId == null)
                notification.addError("El defaultRiskId es obligatorio");

            if (requirementId == null)
                notification.addError("El requirementId es obligatorio");

        }

    }
}

