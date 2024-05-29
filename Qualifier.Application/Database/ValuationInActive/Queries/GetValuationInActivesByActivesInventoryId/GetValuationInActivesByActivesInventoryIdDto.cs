namespace Qualifier.Application.Database.ValuationInActive.Queries.GetValuationInActivesByActivesInventoryId
{
    public class GetValuationInActivesByActivesInventoryIdDto
    {
        public int valuationInActiveId { get; set; }
        public int activesInventoryId { get; set; }
        public int impactValuationId { get; set; }
        public decimal value { get; set; }
        public GetValuationInActivesByActivesInventoryIdImpactValuationDto? impactValuation { get; set; }
    }
    public class GetValuationInActivesByActivesInventoryIdImpactValuationDto
    {
        public string name { get; set; }

    }
}

