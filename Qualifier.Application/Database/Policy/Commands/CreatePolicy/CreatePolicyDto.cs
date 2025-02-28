using Qualifier.Common.Application.NotificationPattern;

namespace Qualifier.Application.Database.Policy.Commands.CreatePolicy
{
    public class CreatePolicyDto
    {
        public int policyId { get; set; }
        public bool isCurrent { get; set; }
        public DateTime date { get; set; }
        public string name { get; set; }
        public string? description { get; set; }
        public int? standardId { get; set; }
        public int? companyId { get; set; }
        public int? creationUserId { get; set; }

        public void requiredFieldsValidation(Notification notification)
        {
            if (isCurrent == null)
                notification.addError("El isCurrent es obligatorio");

            if (date == null)
                notification.addError("El date es obligatorio");

            if (name == null)
                notification.addError("El name es obligatorio");

        }

    }
}

