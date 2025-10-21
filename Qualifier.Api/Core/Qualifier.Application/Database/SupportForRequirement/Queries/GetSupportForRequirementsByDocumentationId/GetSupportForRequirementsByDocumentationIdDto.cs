namespace Qualifier.Application.Database.SupportForRequirement.Queries.GetSupportForRequirementsByDocumentationId
{
    public class GetSupportForRequirementsByDocumentationIdDto
    {
        public int supportForRequirementId { get; set; }
        public int documentationId { get; set; }
        public int requirementId { get; set; }
        public GetSupportForRequirementsByDocumentationIdRequirementDto? requirement { get; set; }
    }
    public class GetSupportForRequirementsByDocumentationIdRequirementDto
    {
        public int numeration { get; set; }
        public string name { get; set; }

    }
}
