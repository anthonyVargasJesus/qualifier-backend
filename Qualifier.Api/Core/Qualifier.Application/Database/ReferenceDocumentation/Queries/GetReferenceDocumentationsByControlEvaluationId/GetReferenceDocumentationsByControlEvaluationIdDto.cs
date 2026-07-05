namespace Qualifier.Application.Database.ReferenceDocumentation.Queries.GetReferenceDocumentationsByControlEvaluationId
{
    public class GetReferenceDocumentationsByControlEvaluationIdDto
    {
        public int referenceDocumentationId { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public string description { get; set; }
        public int? documentationId { get; set; }
        public string? evidenceType { get; set; }
        public long? fileSizeBytes { get; set; }
        public DateTime? creationDate { get; set; }
        public string? creationUserEmail { get; set; }
        public bool isObsolete { get; set; }
        public GetReferenceDocumentationsByControlEvaluationIdDocumentationDto? documentation { get; set; }
    }
    public class GetReferenceDocumentationsByControlEvaluationIdDocumentationDto
    {
        public string name { get; set; }

    }
}

