namespace Qualifier.Application.Database.ReferenceDocumentation.Queries.GetReferenceDocumentationsByControlEvaluationId
{
    public class GetReferenceDocumentationsByControlEvaluationIdDto
    {
        public int referenceDocumentationId { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public string description { get; set; }
        public int documentationId { get; set; }
        public GetReferenceDocumentationsByControlEvaluationIdDocumentationDto? documentation { get; set; }
    }
    public class GetReferenceDocumentationsByControlEvaluationIdDocumentationDto
    {
        public string name { get; set; }

    }
}

