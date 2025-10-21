using Qualifier.Common.Domain.Entities;

namespace Qualifier.Domain.Entities
{
    public class ConfidentialityLevelEntity : BaseEntity
    {
        public int confidentialityLevelId { get; set; }
        public string name { get; set; }
        public int companyId { get; set; }
    }
}

