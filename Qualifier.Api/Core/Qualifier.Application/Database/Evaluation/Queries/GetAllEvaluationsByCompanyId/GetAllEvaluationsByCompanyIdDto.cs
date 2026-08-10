namespace Qualifier.Application.Database.Evaluation.Queries.GetAllEvaluationsByCompanyId
{
    public class GetAllEvaluationsByCompanyIdDto
    {
        public int evaluationId { get; set; }
        public string description { get; set; }
        public int standardId { get; set; }
        public GetAllEvaluationsByCompanyIdStandardDto? standard { get; set; }
    }

    public class GetAllEvaluationsByCompanyIdStandardDto
    {
        public string? name { get; set; }
    }

}
