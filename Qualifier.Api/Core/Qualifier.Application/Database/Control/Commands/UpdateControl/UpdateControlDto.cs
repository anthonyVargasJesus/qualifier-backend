using Qualifier.Common.Application.NotificationPattern;

namespace Qualifier.Application.Database.Control.Commands.UpdateControl
{
    public class UpdateControlDto
    {
        public int? number { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int? controlGroupId { get; set; }
        public int? updateUserId { get; set; }

        public void requiredFieldsValidation(Notification notification)
        {
            if (number == null)
                notification.addError("El number es obligatorio");

            if (name == null)
                notification.addError("El name es obligatorio");

            if (controlGroupId == null)
                notification.addError("El controlGroupId es obligatorio");

        }

    }
}

