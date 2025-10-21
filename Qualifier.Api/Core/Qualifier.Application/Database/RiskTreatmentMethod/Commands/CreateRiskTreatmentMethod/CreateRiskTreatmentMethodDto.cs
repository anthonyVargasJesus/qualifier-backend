using Qualifier.Common.Application.NotificationPattern;

namespace Qualifier.Application.Database.RiskTreatmentMethod.Commands.CreateRiskTreatmentMethod
{
    public class CreateRiskTreatmentMethodDto
    {
        public int riskTreatmentMethodId { get; set; }
        public string name { get; set; }
        public string? description { get; set; }
        public int? companyId { get; set; }
        public int? creationUserId { get; set; }

        public void requiredFieldsValidation(Notification notification)
        {
            if (name == null)
                notification.addError("El name es obligatorio");

        }

    }
}

