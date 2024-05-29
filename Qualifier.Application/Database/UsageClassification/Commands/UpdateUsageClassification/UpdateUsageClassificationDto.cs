using Qualifier.Common.Application.NotificationPattern;

namespace Qualifier.Application.Database.UsageClassification.Commands.UpdateUsageClassification
{
    public class UpdateUsageClassificationDto
    {
        public int usageClassificationId { get; set; }
        public string name { get; set; }
        public int? updateUserId { get; set; }

        public void requiredFieldsValidation(Notification notification)
        {
            if (name == null)
                notification.addError("El name es obligatorio");

        }

    }
}

