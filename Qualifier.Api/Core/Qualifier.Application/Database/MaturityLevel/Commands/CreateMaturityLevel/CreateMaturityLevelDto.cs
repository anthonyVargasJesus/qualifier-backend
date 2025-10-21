
using Qualifier.Common.Application.NotificationPattern;

namespace Qualifier.Application.Database.MaturityLevel.Commands.CreateMaturityLevel
{
    public class CreateMaturityLevelDto
    {
        public string? name { get; set; }
        public string? description { get; set; }
        public string? abbreviation { get; set; }
        public decimal? value { get; set; }
        public string? color { get; set; }
        public int companyId { get; set; }
        public int? creationUserId { get; set; }
        public void requiredFieldsValidation(Notification notification)
        {
            if (name == null || name == "")
                notification.addError("El nombre es obligatorio");

            if (description == null || description == "")
                notification.addError("La descripción es obligatoria");

            if (abbreviation == null || abbreviation == "")
                notification.addError("La abreviatura es obligatoria");

            if (value == null)
                notification.addError("El valor debe ser mayor a 0");

            if (color == null || color == "")
                notification.addError("El color es obligatorio");
        }

    }
}
