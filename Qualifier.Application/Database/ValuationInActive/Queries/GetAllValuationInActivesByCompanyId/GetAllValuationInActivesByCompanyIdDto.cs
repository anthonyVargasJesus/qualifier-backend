namespace Qualifier.Application.Database.ValuationInActive.Queries.GetAllValuationInActivesByCompanyId
{
    public class GetAllValuationInActivesByCompanyIdDto
    {
        public int valuationInActiveId { get; set; }
        public int activesInventoryId { get; set; }
        public int impactValuationId { get; set; }
        public decimal value { get; set; }
        public GetAllValuationInActivesByCompanyIdImpactValuationDto? impactValuation { get; set; }
    }
    public class GetAllValuationInActivesByCompanyIdImpactValuationDto
    {

        public string name { get; set; }

    }
}
