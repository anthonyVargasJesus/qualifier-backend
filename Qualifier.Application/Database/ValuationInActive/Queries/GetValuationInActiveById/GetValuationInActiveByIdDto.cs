namespace Qualifier.Application.Database.ValuationInActive.Queries.GetValuationInActiveById
{
    public class GetValuationInActiveByIdDto
    {
        public int valuationInActiveId { get; set; }
        public int activesInventoryId { get; set; }
        public int impactValuationId { get; set; }
        public decimal value { get; set; }

    }
}

