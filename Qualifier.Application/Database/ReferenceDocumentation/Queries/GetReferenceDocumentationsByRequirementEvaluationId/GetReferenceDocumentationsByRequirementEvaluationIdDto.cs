namespace Qualifier.Application.Database.ReferenceDocumentation.Queries.GetReferenceDocumentationsByRequirementEvaluationId
{
    public class GetReferenceDocumentationsByRequirementEvaluationIdDto
    {
        public int referenceDocumentationId { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public GetReferenceDocumentationsByRequirementEvaluationIdDocumentationDto? documentation { get; set; }
    }
    public class GetReferenceDocumentationsByRequirementEvaluationIdDocumentationDto
    {
        public string name { get; set; }

    }
}

