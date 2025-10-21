using Qualifier.Common.Domain.Entities;

namespace Qualifier.Domain.Entities
{
    public class BreachSeverityEntity : BaseEntity
    {
        public int breachSeverityId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string abbreviation { get; set; }
        public decimal value { get; set; }
        public string color { get; set; }
        public int? companyId { get; set; }
    }
}

