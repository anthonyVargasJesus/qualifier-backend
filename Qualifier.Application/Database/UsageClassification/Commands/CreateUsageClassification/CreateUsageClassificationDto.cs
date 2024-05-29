using Qualifier.Common.Application.NotificationPattern;

namespace Qualifier.Application.Database.UsageClassification.Commands.CreateUsageClassification
{
    public class CreateUsageClassificationDto
    {
        public int usageClassificationId { get; set; }
        public string name { get; set; }
        public int companyId { get; set; }
        public int? creationUserId { get; set; }

        public void requiredFieldsValidation(Notification notification)
        {
            if (name == null)
                notification.addError("El name es obligatorio");

        }

    }
}

