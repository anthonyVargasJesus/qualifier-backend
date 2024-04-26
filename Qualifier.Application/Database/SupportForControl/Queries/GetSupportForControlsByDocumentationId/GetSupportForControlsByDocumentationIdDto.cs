namespace Qualifier.Application.Database.SupportForControl.Queries.GetSupportForControlsByDocumentationId
{
    public class GetSupportForControlsByDocumentationIdDto
    {
        public int supportForControlId { get; set; }
        public int documentationId { get; set; }
        public int controlId { get; set; }
        public GetSupportForControlsByDocumentationIdControlDto? control { get; set; }
    }
    public class GetSupportForControlsByDocumentationIdControlDto
    {
        public int number { get; set; }
        public string name { get; set; }

    }
}
