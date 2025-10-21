namespace Qualifier.Application.Database.DocumentType.Queries.GetDocumentTypesByCompanyId
{
    public class GetDocumentTypesByCompanyIdDto
    {
        public int documentTypeId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int companyId { get; set; }

    }
}

