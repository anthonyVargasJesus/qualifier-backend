using Qualifier.Common.Application.NotificationPattern;

namespace Qualifier.Application.Database.ImpactValuation.Commands.CreateImpactValuation
{
    public class CreateImpactValuationDto
    {
        public string abbreviation { get; set; }
        public string name { get; set; }
        public decimal? minimumValue { get; set; }
        public decimal? maximumValue { get; set; }
        public decimal defaultValue { get; set; }
        public int companyId { get; set; }
        public int? creationUserId { get; set; }

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

