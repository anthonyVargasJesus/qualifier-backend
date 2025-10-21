namespace Qualifier.Application.Database.SupportForRequirement.Queries.GetSupportForRequirementById
{
    public class GetSupportForRequirementByIdDto
    {
        public int supportForRequirementId { get; set; }
        public int documentationId { get; set; }
        public int requirementId { get; set; }
        public int standardId { get; set; }
        public int companyId { get; set; }

    }
}

