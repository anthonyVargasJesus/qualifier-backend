using Qualifier.Common.Application.NotificationPattern;

namespace Qualifier.Application.Database.ControlInDefaultRisk.Commands.UpdateControlInDefaultRisk
{
    public class UpdateControlInDefaultRiskDto
    {
        public int controlInDefaultRiskId { get; set; }
        public int defaultRiskId { get; set; }
        public int controlId { get; set; }
        public bool? isActive { get; set; }
        public int? updateUserId { get; set; }

        public void requiredFieldsValidation(Notification notification)
        {
            if (defaultRiskId == null)
                notification.addError("El defaultRiskId es obligatorio");

            if (controlId == null)
                notification.addError("El controlId es obligatorio");

        }

    }
}

