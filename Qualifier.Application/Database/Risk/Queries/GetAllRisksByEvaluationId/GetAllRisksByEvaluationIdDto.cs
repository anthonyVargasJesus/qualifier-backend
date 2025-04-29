namespace Qualifier.Application.Database.Risk.Queries.GetAllRisksByEvaluationId
{
    public class GetAllRisksByEvaluationIdDto
    {
        public int riskId { get; set; }
        public int evaluationId { get; set; }
        public int activesInventoryId { get; set; }
        public string activesInventoryNumber { get; set; }
        public string activesInventoryName { get; set; }
        public int menaceId { get; set; }
        public int vulnerabilityId { get; set; }

    }
}

