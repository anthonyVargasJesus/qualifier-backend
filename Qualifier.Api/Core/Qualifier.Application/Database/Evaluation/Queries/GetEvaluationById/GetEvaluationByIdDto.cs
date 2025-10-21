using Qualifier.Application.Database.Evaluation.Queries.GetEvaluationsByCompanyId;

namespace Qualifier.Application.Database.Evaluation.Queries.GetEvaluationById
{
    public class GetEvaluationByIdDto
    {
        public int evaluationId { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public string description { get; set; }
        public int? referenceEvaluationId { get; set; }
        public bool? isGapAnalysis { get; set; }
        public int standardId { get; set; }
        public bool isCurrent { get; set; }
        public GetEvaluationByIdDtoStandardDto? standard { get; set; }
    }

    public class GetEvaluationByIdDtoStandardDto
    {
        public string? name { get; set; }

    }
}

