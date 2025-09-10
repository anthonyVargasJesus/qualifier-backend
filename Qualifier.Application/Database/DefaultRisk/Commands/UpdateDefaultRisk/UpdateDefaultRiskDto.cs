using Qualifier.Common.Application.NotificationPattern;

namespace Qualifier.Application.Database.DefaultRisk.Commands.UpdateDefaultRisk
{
    public class UpdateDefaultRiskDto
    {
        public int defaultRiskId { get; set; }
        public int? standardId { get; set; }
        public string name { get; set; }
        public int menaceId { get; set; }
        public int vulnerabilityId { get; set; }
        public int? updateUserId { get; set; }

        public void requiredFieldsValidation(Notification notification)
        {
            if (name == null)
                notification.addError("El name es obligatorio");

            if (menaceId == null)
                notification.addError("El menaceId es obligatorio");

            if (vulnerabilityId == null)
                notification.addError("El vulnerabilityId es obligatorio");

        }

    }
}

