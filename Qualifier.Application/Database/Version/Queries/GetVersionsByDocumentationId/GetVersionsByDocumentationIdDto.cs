namespace Qualifier.Application.Database.Version.Queries.GetVersionsByDocumentationId
{
    public class GetVersionsByDocumentationIdDto
    {
        public int versionId { get; set; }
        public decimal number { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public DateTime date { get; set; }
        public bool isCurrent { get; set; }
        public string fileName { get; set; }
        public GetVersionsByDocumentationIdConfidentialityLevelDto? confidentialityLevel { get; set; }
    }
    public class GetVersionsByDocumentationIdConfidentialityLevelDto
    {
        public string name { get; set; }

    }
}
