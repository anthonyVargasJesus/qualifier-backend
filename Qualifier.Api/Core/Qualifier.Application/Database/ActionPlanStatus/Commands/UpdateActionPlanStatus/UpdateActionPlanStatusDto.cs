using Qualifier.Common.Application.NotificationPattern;

namespace Qualifier.Application.Database.ActionPlanStatus.Commands.UpdateActionPlanStatus
{
    public class UpdateActionPlanStatusDto
    {
        public int actionPlanStatusId { get; set; }
        public string? name { get; set; }
        public string? description { get; set; }
        public string? abbreviation { get; set; }
        public decimal? value { get; set; }
        public string? color { get; set; }
        public int? updateUserId { get; set; }

        public void requiredFieldsValidation(Notification notification)
        {
            if (name == null)
                notification.addError("El name es obligatorio");

            if (description == null)
                notification.addError("El description es obligatorio");

            if (abbreviation == null)
                notification.addError("El abbreviation es obligatorio");

            if (value == null && value <= 0)
                notification.addError("El value no puede ser 0");

            if (color == null)
                notification.addError("El color es obligatorio");

        }

    }
}

