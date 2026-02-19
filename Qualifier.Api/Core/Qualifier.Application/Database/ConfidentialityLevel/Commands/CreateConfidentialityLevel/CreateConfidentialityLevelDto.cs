using Qualifier.Common.Application.NotificationPattern;

namespace Qualifier.Application.Database.ConfidentialityLevel.Commands.CreateConfidentialityLevel
{
    public class CreateConfidentialityLevelDto
    {
        public int? confidentialityLevelId { get; set; }
        public string? name { get; set; }
        public int? companyId { get; set; }
        public int? creationUserId { get; set; }

        public void requiredFieldsValidation(Notification notification)
        {
        }

    }
}

