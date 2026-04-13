using Qualifier.Common.Domain.Entities;

namespace Qualifier.Domain.Entities
{
    public class ControlTypeEntity : BaseEntity
    {
        public int controlTypeId { get; set; }
        public string name { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;
        public string abbreviation { get; set; } = string.Empty;
        public decimal? factor { get; set; }
        public decimal minimum { get; set; }
        public decimal? maximum { get; set; }
        public string color { get; set; } = string.Empty;
        public int? companyId { get; set; }
    }
}
