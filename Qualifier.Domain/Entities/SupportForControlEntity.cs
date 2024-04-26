using Qualifier.Common.Domain.Entities;

namespace Qualifier.Domain.Entities
{
    public class SupportForControlEntity : BaseEntity
    {
        public int supportForControlId { get; set; }
        public int documentationId { get; set; }
        public int controlId { get; set; }
        public int standardId { get; set; }
        public int companyId { get; set; }
        public DocumentationEntity documentation { get; set; }
        public ControlEntity control { get; set; }
    }
}

