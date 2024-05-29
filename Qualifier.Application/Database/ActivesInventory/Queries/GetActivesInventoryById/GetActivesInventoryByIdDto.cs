namespace Qualifier.Application.Database.ActivesInventory.Queries.GetActivesInventoryById
{
    public class GetActivesInventoryByIdDto
    {
        public int activesInventoryId { get; set; }
        public string number { get; set; }
        public int macroprocessId { get; set; }
        public int subprocessId { get; set; }
        public string procedure { get; set; }
        public int activeTypeId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int ownerId { get; set; }
        public int custodianId { get; set; }
        public int usageClassificationId { get; set; }
        public int supportTypeId { get; set; }
        public int locationId { get; set; }
        public decimal valuation { get; set; }

    }
}

