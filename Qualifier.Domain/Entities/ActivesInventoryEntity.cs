using Qualifier.Common.Domain.Entities;

namespace Qualifier.Domain.Entities
{
    public class ActivesInventoryEntity : BaseEntity
    {
        public int activesInventoryId { get; set; }
        public string number { get; set; }
        public int macroprocessId { get; set; }
        public int subprocessId { get; set; }
        public string? procedure { get; set; }
        public int activeTypeId { get; set; }
        public string name { get; set; }
        public string? description { get; set; }
        public int ownerId { get; set; }
        public int custodianId { get; set; }
        public int usageClassificationId { get; set; }
        public int supportTypeId { get; set; }
        public int locationId { get; set; }
        public decimal? valuation { get; set; }
        public int? companyId { get; set; }
        public MacroprocessEntity macroprocess { get; set; }
        public SubprocessEntity subprocess { get; set; }
        public ActiveTypeEntity activeType { get; set; }
        public OwnerEntity owner { get; set; }
        public CustodianEntity custodian { get; set; }
        public UsageClassificationEntity usageClassification { get; set; }
        public SupportTypeEntity supportType { get; set; }
        public LocationEntity location { get; set; }
    }
}

