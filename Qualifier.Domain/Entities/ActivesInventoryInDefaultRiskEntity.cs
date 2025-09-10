using Qualifier.Common.Domain.Entities;

namespace Qualifier.Domain.Entities
{
    public class ActivesInventoryInDefaultRiskEntity : BaseEntity
    {
        public int activesInventoryInDefaultRiskId { get; set; }
        public int defaultRiskId { get; set; }
        public int activesInventoryId { get; set; }
        public bool? isActive { get; set; }
        public int? companyId { get; set; }
        public DefaultRiskEntity defaultRisk { get; set; }
        public ActivesInventoryEntity activesInventory { get; set; }
    }
}

