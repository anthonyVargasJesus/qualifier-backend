using Qualifier.Common.Application.NotificationPattern;

namespace Qualifier.Application.Database.DefaultRisk.Commands.CreateDefaultRisk
{
    public class CreateDefaultRiskDto
    {
        public int defaultRiskId { get; set; }
        public int? standardId { get; set; }
        public string? name { get; set; }
        public int? menaceId { get; set; }
        public int? vulnerabilityId { get; set; }
        public int? companyId { get; set; }
        public int? creationUserId { get; set; }

        public void requiredFieldsValidation(Notification notification)
        {
            if (name == null)
                notification.addError("El name es obligatorio");

            if (menaceId == null && menaceId <= 0)
                notification.addError("El menaceId es obligatorio");

            if (vulnerabilityId == null && vulnerabilityId <= 0)
                notification.addError("El vulnerabilityId es obligatorio");

        }

    }
}

