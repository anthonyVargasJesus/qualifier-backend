using Qualifier.Common.Domain.Entities;

namespace Qualifier.Domain.Entities
{
    public class ValuationInActiveEntity : BaseEntity
    {
        public int valuationInActiveId { get; set; }
        public int activesInventoryId { get; set; }
        public int impactValuationId { get; set; }
        public decimal value { get; set; }
        public int? companyId { get; set; }
        public ActivesInventoryEntity activesInventory { get; set; }
        public ImpactValuationEntity impactValuation { get; set; }
    }
}

