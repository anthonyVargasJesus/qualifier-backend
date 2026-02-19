using Qualifier.Common.Application.NotificationPattern;

namespace Qualifier.Application.Database.ControlGroup.Commands.UpdateControlGroup
{
    public class UpdateControlGroupDto
    {
        public int? number { get; set; }
        public string? name { get; set; }
        public string? description { get; set; }
        public int? updateUserId { get; set; }

        public void requiredFieldsValidation(Notification notification)
        {
            if (number == null)
                notification.addError("El number es obligatorio");

            if (name == null)
                notification.addError("El name es obligatorio");

        }

    }
}

