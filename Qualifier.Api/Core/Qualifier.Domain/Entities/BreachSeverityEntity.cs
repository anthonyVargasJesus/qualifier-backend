using Qualifier.Common.Domain.Entities;

namespace Qualifier.Domain.Entities
{
    public class BreachSeverityEntity : BaseEntity
    {
        public int breachSeverityId { get; set; }
        public string name { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;
        public string abbreviation { get; set; } = string.Empty;
        public decimal value { get; set; }
        public string color { get; set; } = string.Empty;
        public int? companyId { get; set; }
    }
}

