using Qualifier.Common.Domain.Entities;

namespace Qualifier.Domain.Entities
{
    public class RiskTreatmentMethodEntity : BaseEntity
    {
        public int riskTreatmentMethodId { get; set; }
        public string name { get; set; }
        public string? description { get; set; }
        public int? companyId { get; set; }
    }
}

