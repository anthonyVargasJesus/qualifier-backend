using Qualifier.Common.Domain.Entities;

namespace Qualifier.Domain.Entities
{
    public class PolicyEntity : BaseEntity
    {
        public int policyId { get; set; }
        public bool isCurrent { get; set; }
        public DateTime date { get; set; }
        public string name { get; set; }
        public string? description { get; set; }
        public int? standardId { get; set; }
        public int? companyId { get; set; }
    }
}

