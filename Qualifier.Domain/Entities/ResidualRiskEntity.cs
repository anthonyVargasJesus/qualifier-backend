using Qualifier.Common.Domain.Entities;

namespace Qualifier.Domain.Entities
{
    public class ResidualRiskEntity : BaseEntity
    {
        public int residualRiskId { get; set; }
        public string name { get; set; }
        public string? description { get; set; }
        public string abbreviation { get; set; }
        public decimal? factor { get; set; }
        public decimal? minimum { get; set; }
        public decimal? maximum { get; set; }
        public string color { get; set; }
        public int? companyId { get; set; }
    }
}

