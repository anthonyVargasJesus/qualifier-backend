namespace Qualifier.Application.Database.ReferenceDocumentation.Queries.GetReferenceDocumentationsByRequirementEvaluationId
{
    public class GetReferenceDocumentationsByRequirementEvaluationIdDto
    {
        public int referenceDocumentationId { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public string? description { get; set; }
        public string? evidenceType { get; set; }
        public long? fileSizeBytes { get; set; }
        public DateTime? creationDate { get; set; }
        public string? creationUserEmail { get; set; }
        public bool isObsolete { get; set; }
        public GetReferenceDocumentationsByRequirementEvaluationIdDocumentationDto? documentation { get; set; }
    }
    public class GetReferenceDocumentationsByRequirementEvaluationIdDocumentationDto
    {
        public string name { get; set; }

    }
}

