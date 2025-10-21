using Qualifier.Common.Application.NotificationPattern;

namespace Qualifier.Application.Database.RiskStatus.Commands.UpdateRiskStatus
{
    public class UpdateRiskStatusDto
    {
        public int riskStatusId { get; set; }
        public string name { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;
        public string abbreviation { get; set; } = string.Empty;
        public decimal value { get; set; }
        public string color { get; set; } = string.Empty;
        public int? updateUserId { get; set; }

        public void requiredFieldsValidation(Notification notification)
        {
            if (name == null)
                notification.addError("El name es obligatorio");

            if (description == null)
                notification.addError("El description es obligatorio");

            if (abbreviation == null)
                notification.addError("El abbreviation es obligatorio");

            if (value == 0)
                notification.addError("El value no puede ser 0");

            if (color == null)
                notification.addError("El color es obligatorio");

        }

    }
}

