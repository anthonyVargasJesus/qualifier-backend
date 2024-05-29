namespace Qualifier.Application.Database.ImpactValuation.Queries.GetImpactValuationsByCompanyId
{
    public class GetImpactValuationsByCompanyIdDto
    {
        public int impactValuationId { get; set; }
        public string abbreviation { get; set; }
        public string name { get; set; }
        public decimal minimumValue { get; set; }
        public decimal maximumValue { get; set; }
        public decimal defaultValue { get; set; }

    }
}

