namespace Qualifier.Application.Database.Risk.Queries.GetRiskById
{
    public class GetRiskByIdDto
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

    }
}

