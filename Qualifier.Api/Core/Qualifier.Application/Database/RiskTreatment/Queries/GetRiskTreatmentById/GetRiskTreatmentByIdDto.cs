namespace Qualifier.Application.Database.RiskTreatment.Queries.GetRiskTreatmentById
{
    public class GetRiskTreatmentByIdDto
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

    }
}

