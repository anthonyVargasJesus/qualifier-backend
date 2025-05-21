using Qualifier.Common.Application.NotificationPattern;

namespace Qualifier.Application.Database.RiskTreatment.Commands.CreateRiskTreatment
{
    public class CreateRiskTreatmentDto
    {
        public int riskTreatmentId { get; set; }
        public int riskId { get; set; }
        public int riskTreatmentMethodId { get; set; }
        public string controlType { get; set; }
        public string? controlsToImplement { get; set; }
        public decimal? menaceLevelValue { get; set; }
        public decimal? vulnerabilityLevelValue { get; set; }
        public decimal? riskAssessmentValue { get; set; }
        public int? riskLevelId { get; set; }
        public int? residualRiskId { get; set; }
        public int? companyId { get; set; }
        public int? creationUserId { get; set; }

        public void requiredFieldsValidation(Notification notification)
        {
            if (riskId == null)
                notification.addError("El riskId es obligatorio");

            if (riskTreatmentMethodId == null)
                notification.addError("El riskTreatmentMethodId es obligatorio");

            if (controlType == null)
                notification.addError("El controlType es obligatorio");

        }

    }
}

