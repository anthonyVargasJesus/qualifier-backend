using Qualifier.Common.Application.NotificationPattern;

namespace Qualifier.Application.Database.Risk.Commands.UpdateRisk
{
    public class UpdateRiskDto
    {
        public int riskId { get; set; }
        public int activesInventoryId { get; set; }
        public string? activesInventoryNumber { get; set; }
        public string? macroProcess { get; set; }
        public string? subProcess { get; set; }
        public string? activesInventoryName { get; set; }
        public decimal? activesInventoryValuation { get; set; }
        public int menaceId { get; set; }
        public int vulnerabilityId { get; set; }
        public decimal menaceLevel { get; set; }
        public decimal vulnerabilityLevel { get; set; }
        public int controlId { get; set; }
        public decimal riskAssessmentValue { get; set; }
        public int riskLevelId { get; set; }
        public string treatmentMethod { get; set; }
        public int controlTypeId { get; set; }
        public string? controlsToImplement { get; set; }
        public decimal? menaceLevelWithTreatment { get; set; }
        public decimal? vulnerabilityLevelWithTreatment { get; set; }
        public decimal riskAssessmentValueWithTreatment { get; set; }
        public int riskLevelWithImplementedControlld { get; set; }
        public string residualRisk { get; set; }
        public string? activities { get; set; }
        public DateTime? implementationStartDate { get; set; }
        public DateTime? verificationDate { get; set; }
        public int? responsibleId { get; set; }
        public string? observation { get; set; }
        public int? updateUserId { get; set; }

        public void requiredFieldsValidation(Notification notification)
        {
            if (activesInventoryId == null)
                notification.addError("El activesInventoryId es obligatorio");

            if (menaceId == null)
                notification.addError("El menaceId es obligatorio");

            if (vulnerabilityId == null)
                notification.addError("El vulnerabilityId es obligatorio");

            if (menaceLevel == null)
                notification.addError("El menaceLevel es obligatorio");

            if (vulnerabilityLevel == null)
                notification.addError("El vulnerabilityLevel es obligatorio");

            if (controlId == null)
                notification.addError("El controlId es obligatorio");

            if (riskAssessmentValue == null)
                notification.addError("El riskAssessmentValue es obligatorio");

            if (riskLevelId == null)
                notification.addError("El riskLevelId es obligatorio");

            if (treatmentMethod == null)
                notification.addError("El treatmentMethod es obligatorio");

            if (controlTypeId == null)
                notification.addError("El controlTypeId es obligatorio");

            if (riskAssessmentValueWithTreatment == null)
                notification.addError("El riskAssessmentValueWithTreatment es obligatorio");

            if (riskLevelWithImplementedControlld == null)
                notification.addError("El riskLevelWithImplementedControlld es obligatorio");

            if (residualRisk == null)
                notification.addError("El residualRisk es obligatorio");

        }

    }
}

