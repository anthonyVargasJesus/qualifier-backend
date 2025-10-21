using Qualifier.Common.Domain.Entities;

namespace Qualifier.Domain.Entities
{
    public class ImpactValuationEntity : BaseEntity
    {
        public int impactValuationId { get; set; }
        public string abbreviation { get; set; }
        public string name { get; set; }
        public decimal? minimumValue { get; set; }
        public decimal? maximumValue { get; set; }
        public decimal defaultValue { get; set; }
        public int companyId { get; set; }
    }
}

