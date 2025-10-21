using NPOI.SS.Formula.Functions;

namespace Qualifier.Application.Database.Risk.Queries.GetRisksByEvaluationId
{

    public class GetRisksByEvaluationIdResponse<T>
    {
        public List<T>? data { get; set; }
        public Object? pagination { get; set; }
        public int evaluationId { get; set; }
    }
    public class GetRisksByEvaluationIdDto
    {
        public int riskId { get; set; }
        public string? name { get; set; }
        public int activesInventoryId { get; set; }
        public string activesInventoryNumber { get; set; }
        public string activesInventoryName { get; set; }
        public GetRisksByEvaluationIdMenaceDto? menace { get; set; }
        public GetRisksByEvaluationIdVulnerabilityDto? vulnerability { get; set; }
        public int riskAssessmentId { get; set; }
        public int riskTreatmentId { get; set; }
        public string? controlSummary { get; set; }
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

