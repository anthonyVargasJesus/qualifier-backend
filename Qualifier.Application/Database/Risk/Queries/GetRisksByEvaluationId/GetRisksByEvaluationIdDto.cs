namespace Qualifier.Application.Database.Risk.Queries.GetRisksByEvaluationId
{
    public class GetRisksByEvaluationIdDto
    {
        public int riskId { get; set; }
        public int activesInventoryId { get; set; }
        public string activesInventoryNumber { get; set; }
        public string activesInventoryName { get; set; }
        public GetRisksByEvaluationIdMenaceDto? menace { get; set; }
        public GetRisksByEvaluationIdVulnerabilityDto? vulnerability { get; set; }
    }
    public class GetRisksByEvaluationIdMenaceDto
    {
        public string name { get; set; }

    }
    public class GetRisksByEvaluationIdVulnerabilityDto
    {
        public string name { get; set; }

    }
}

