namespace Qualifier.Application.Database.MaturityLevel.Queries.GetMaturityLevelsByCompanyId
{
    public class GetMaturityLevelsByCompanyIdDto
    {
        public int maturityLevelId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string abbreviation { get; set; }
        public decimal value { get; set; }
        public string color { get; set; }

    }
}

