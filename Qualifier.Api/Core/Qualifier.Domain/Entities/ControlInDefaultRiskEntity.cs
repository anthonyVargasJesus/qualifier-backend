using Qualifier.Common.Domain.Entities;

namespace Qualifier.Domain.Entities
{
    public class ControlInDefaultRiskEntity : BaseEntity
    {
        public int controlInDefaultRiskId { get; set; }
        public int defaultRiskId { get; set; }
        public int controlId { get; set; }
        public bool? isActive { get; set; }
        public int? companyId { get; set; }
        public DefaultRiskEntity defaultRisk { get; set; }
        public ControlEntity control { get; set; }
    }
}

