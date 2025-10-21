using Qualifier.Common.Application.NotificationPattern;

namespace Qualifier.Application.Database.RiskAssessment.Commands.UpdateRiskAssessment
{
    public class UpdateRiskAssessmentDto
    {
        public int riskAssessmentId { get; set; }
        public int riskId { get; set; }
        public decimal valuationCID { get; set; }
        public decimal menaceLevelValue { get; set; }
        public decimal vulnerabilityLevelValue { get; set; }
        public string existingImplementedControls { get; set; }
        public decimal riskAssessmentValue { get; set; }
        public int riskLevelId { get; set; }
        public int? updateUserId { get; set; }

        public void requiredFieldsValidation(Notification notification)
        {
            if (riskId == null)
                notification.addError("El riskId es obligatorio");

            if (valuationCID == null)
                notification.addError("El valuationCID es obligatorio");

            if (menaceLevelValue == null)
                notification.addError("El menaceLevelValue es obligatorio");

            if (vulnerabilityLevelValue == null)
                notification.addError("El vulnerabilityLevelValue es obligatorio");

            if (existingImplementedControls == null)
                notification.addError("El existingImplementedControls es obligatorio");

            if (riskAssessmentValue == null)
                notification.addError("El riskAssessmentValue es obligatorio");

            if (riskLevelId == null)
                notification.addError("El riskLevelId es obligatorio");

        }

    }
}

