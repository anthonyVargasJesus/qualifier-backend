using Qualifier.Common.Application.NotificationPattern;

namespace Qualifier.Application.Database.ControlInDefaultRisk.Commands.CreateControlInDefaultRisk
{
    public class CreateControlInDefaultRiskDto
    {
        public int controlInDefaultRiskId { get; set; }
        public int defaultRiskId { get; set; }
        public int controlId { get; set; }
        public bool? isActive { get; set; }
        public int? companyId { get; set; }
        public int? creationUserId { get; set; }

        public void requiredFieldsValidation(Notification notification)
        {
            if (defaultRiskId == null)
                notification.addError("El defaultRiskId es obligatorio");

            if (controlId == null)
                notification.addError("El controlId es obligatorio");

        }

    }
}

