using Qualifier.Common.Domain.Entities;

namespace Qualifier.Domain.Entities
{
    public class DefaultRiskEntity : BaseEntity
    {
        public int defaultRiskId { get; set; }
        public int? standardId { get; set; }
        public string name { get; set; }
        public int menaceId { get; set; }
        public int vulnerabilityId { get; set; }
        public int? companyId { get; set; }
        public MenaceEntity menace { get; set; }
        public VulnerabilityEntity vulnerability { get; set; }
    }
}

