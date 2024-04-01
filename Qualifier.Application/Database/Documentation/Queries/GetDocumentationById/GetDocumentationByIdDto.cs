namespace Qualifier.Application.Database.Documentation.Queries.GetDocumentationById
{
    public class GetDocumentationByIdDto
    {
        public int documentationId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string template { get; set; }
        public int standardId { get; set; }

    }
}

