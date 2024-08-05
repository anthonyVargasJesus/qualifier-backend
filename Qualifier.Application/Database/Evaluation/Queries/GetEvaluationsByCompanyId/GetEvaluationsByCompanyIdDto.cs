namespace Qualifier.Application.Database.Evaluation.Queries.GetEvaluationsByCompanyId
{
    public class GetEvaluationsByCompanyIdDto
    {
        public int evaluationId { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public string description { get; set; }
        public int standardId { get; set; }
        public int evaluationStateId { get; set; }
        public int referenceEvaluationId { get; set; }
        public bool isGapAnalysis { get; set; }
        public bool isCurrent { get; set; }
        public GetEvaluationsByCompanyIdEvaluationStateDto? evaluationState { get; set; }
    }
    public class GetEvaluationsByCompanyIdEvaluationStateDto
    {
        public string name { get; set; }
        public string color { get; set; }
    }
}
