namespace Qualifier.Application.Database.ReferenceDocumentation.Queries.GetReferenceDocumentationById
{
    public class GetReferenceDocumentationByIdDto
    {
        public int referenceDocumentationId { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public string? description { get; set; }
        public int documentationId { get; set; }
        public int? requirementEvaluationId { get; set; }
        public int? controlEvaluationId { get; set; }
        public int? evaluationId { get; set; }

    }
}

