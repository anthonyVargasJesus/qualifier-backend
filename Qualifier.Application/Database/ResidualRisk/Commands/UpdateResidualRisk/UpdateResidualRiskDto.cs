using Qualifier.Common.Application.NotificationPattern;

namespace Qualifier.Application.Database.ResidualRisk.Commands.UpdateResidualRisk
{
    public class UpdateResidualRiskDto
    {
        public int residualRiskId { get; set; }
        public string name { get; set; }
        public string? description { get; set; }
        public string abbreviation { get; set; }
        public decimal? factor { get; set; }
        public decimal? minimum { get; set; }
        public decimal? maximum { get; set; }
        public string color { get; set; }
        public int? updateUserId { get; set; }

        public void requiredFieldsValidation(Notification notification)
        {
            if (name == null)
                notification.addError("El name es obligatorio");

            if (abbreviation == null)
                notification.addError("El abbreviation es obligatorio");

            if (color == null)
                notification.addError("El color es obligatorio");

        }

    }
}

