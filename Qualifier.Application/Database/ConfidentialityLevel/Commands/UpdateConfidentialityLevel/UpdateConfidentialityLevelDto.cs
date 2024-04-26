using Qualifier.Common.Application.NotificationPattern;

namespace Qualifier.Application.Database.ConfidentialityLevel.Commands.UpdateConfidentialityLevel
{
    public class UpdateConfidentialityLevelDto
    {
        public int confidentialityLevelId { get; set; }
        public string name { get; set; }
        public int? updateUserId { get; set; }

        public void requiredFieldsValidation(Notification notification)
        {
            if (name == null)
                notification.addError("El name es obligatorio");

        }

    }
}

