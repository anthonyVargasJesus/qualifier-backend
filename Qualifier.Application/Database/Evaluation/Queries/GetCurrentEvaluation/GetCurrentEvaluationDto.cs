
namespace Qualifier.Application.Database.Evaluation.Queries.GetCurrentEvaluation
{
    public class GetCurrentEvaluationDto
    {
        public int evaluationId { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public string description { get; set; }
        public int? referenceEvaluationId { get; set; }
        public bool? isGapAnalysis { get; set; }
        public int standardId { get; set; }
        public bool isCurrent { get; set; }
        public GetCurrentEvaluationDtoEvaluationStateDto? evaluationState { get; set; }
    }

    public class GetCurrentEvaluationDtoEvaluationStateDto
    {
        public string? name { get; set; }
        public string? color { get; set; }
    }
}
