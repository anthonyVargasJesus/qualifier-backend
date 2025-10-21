using Qualifier.Common.Application.NotificationPattern;

namespace Qualifier.Application.Database.ImpactValuation.Commands.UpdateImpactValuation
{
    public class UpdateImpactValuationDto
    {
        public int impactValuationId { get; set; }
        public string abbreviation { get; set; }
        public string name { get; set; }
        public decimal? minimumValue { get; set; }
        public decimal? maximumValue { get; set; }
        public decimal defaultValue { get; set; }
        public int? updateUserId { get; set; }

        public void requiredFieldsValidation(Notification notification)
        {
            if (abbreviation == null)
                notification.addError("El abbreviation es obligatorio");

            if (name == null)
                notification.addError("El name es obligatorio");

            if (defaultValue == null)
                notification.addError("El defaultValue es obligatorio");

        }

    }
}

