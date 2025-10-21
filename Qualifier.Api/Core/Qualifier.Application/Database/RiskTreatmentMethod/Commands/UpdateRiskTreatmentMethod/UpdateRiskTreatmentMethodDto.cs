using Qualifier.Common.Application.NotificationPattern;

namespace Qualifier.Application.Database.RiskTreatmentMethod.Commands.UpdateRiskTreatmentMethod
{
    public class UpdateRiskTreatmentMethodDto
    {
        public int riskTreatmentMethodId { get; set; }
        public string name { get; set; }
        public string? description { get; set; }
        public int? updateUserId { get; set; }

        public void requiredFieldsValidation(Notification notification)
        {
            if (name == null)
                notification.addError("El name es obligatorio");

        }

    }
}

