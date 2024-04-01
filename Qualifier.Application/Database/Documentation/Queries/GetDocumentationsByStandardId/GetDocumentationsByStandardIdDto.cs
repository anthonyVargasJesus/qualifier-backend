namespace Qualifier.Application.Database.Documentation.Queries.GetDocumentationsByStandardId
{
    public class GetDocumentationsByStandardIdDto
    {
        public int documentationId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string template { get; set; }
        public int requirementId { get; set; }

    }
}

