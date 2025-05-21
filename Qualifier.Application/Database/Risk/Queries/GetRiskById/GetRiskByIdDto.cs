namespace Qualifier.Application.Database.Risk.Queries.GetRiskById
{
    public class GetRiskByIdDto
    {
        public int riskId { get; set; }
        public int evaluationId { get; set; }
        public int activesInventoryId { get; set; }
        public string? activesInventoryNumber { get; set; }
        public string? activesInventoryName { get; set; }
        public int menaceId { get; set; }
        public int vulnerabilityId { get; set; }
        public int riskAssessmentId { get; set; }
        public decimal valuationCID { get; set; }
        public int riskTreatmentId { get; set; }
        public int controlImplementationId { get; set; }
    }
}

