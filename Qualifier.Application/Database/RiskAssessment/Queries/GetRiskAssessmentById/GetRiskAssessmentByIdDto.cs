namespace Qualifier.Application.Database.RiskAssessment.Queries.GetRiskAssessmentById
{
    public class GetRiskAssessmentByIdDto
    {
        public int riskAssessmentId { get; set; }
        public int riskId { get; set; }
        public decimal valuationCID { get; set; }
        public decimal menaceLevelValue { get; set; }
        public decimal vulnerabilityLevelValue { get; set; }
        public string existingImplementedControls { get; set; }
        public decimal riskAssessmentValue { get; set; }
        public int riskLevelId { get; set; }

    }
}

