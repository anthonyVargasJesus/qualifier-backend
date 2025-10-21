using Qualifier.Common.Domain.Entities;

namespace Qualifier.Domain.Entities
{
    public class RequirementInDefaultRiskEntity : BaseEntity
    {
        public int requirementInDefaultRiskId { get; set; }
        public int defaultRiskId { get; set; }
        public int requirementId { get; set; }
        public bool? isActive { get; set; }
        public int? companyId { get; set; }
        public DefaultRiskEntity defaultRisk { get; set; }
    }
}

