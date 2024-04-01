namespace Qualifier.Application.Database.Evaluation.Queries.GetEvaluationsByCompanyId
{
    public class GetEvaluationsByCompanyIdDto
    {
        public int evaluationId { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public string description { get; set; }
        public int standardId { get; set; }
        public GetEvaluationsByCompanyIdStandardDto? standard { get; set; }
    }
    public class GetEvaluationsByCompanyIdStandardDto
    {
        public string? name { get; set; }

    }
}
