namespace Qualifier.Application.Database.Documentation.Queries.GetDocumentationsByCompanyId
{
    public class GetDocumentationsByCompanyIdDto
    {
        public int documentationId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int standardId { get; set; }
        public GetDocumentationsByCompanyIdDocumentTypeDto? documentType { get; set; }
        public GetDocumentationsByCompanyIdStandardDto? standard { get; set; }
    }
    public class GetDocumentationsByCompanyIdDocumentTypeDto
    {
        public string name { get; set; }

    }
    public class GetDocumentationsByCompanyIdStandardDto
    {

        public string name { get; set; }

    }
}
