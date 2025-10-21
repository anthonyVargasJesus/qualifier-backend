using Qualifier.Common.Domain.Entities;

namespace Qualifier.Domain.Entities
{
    public class SupportForRequirementEntity : BaseEntity
    {
        public int supportForRequirementId { get; set; }
        public int documentationId { get; set; }
        public int requirementId { get; set; }
        public int standardId { get; set; }
        public int companyId { get; set; }
        public DocumentationEntity documentation { get; set; }
        public RequirementEntity requirement { get; set; }
    }
}

