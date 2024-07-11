using Qualifier.Common.Application.NotificationPattern;

namespace Qualifier.Application.Database.ControlType.Commands.CreateControlType
{
    public class CreateControlTypeDto
    {
        public int controlTypeId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string abbreviation { get; set; }
        public decimal? factor { get; set; }
        public decimal minimum { get; set; }
        public decimal? maximum { get; set; }
        public string color { get; set; }
        public int? companyId { get; set; }
        public int? creationUserId { get; set; }

        public void requiredFieldsValidation(Notification notification)
        {
            if (name == null)
                notification.addError("El name es obligatorio");

            if (description == null)
                notification.addError("El description es obligatorio");

            if (abbreviation == null)
                notification.addError("El abbreviation es obligatorio");

            if (minimum == null)
                notification.addError("El minimum es obligatorio");

            if (color == null)
                notification.addError("El color es obligatorio");

        }


    }
}

