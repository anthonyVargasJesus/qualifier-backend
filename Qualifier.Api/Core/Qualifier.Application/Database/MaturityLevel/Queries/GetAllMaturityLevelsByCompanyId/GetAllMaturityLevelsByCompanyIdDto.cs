namespace Qualifier.Application.Database.MaturityLevel.Queries.GetAllMaturityLevelsByCompanyId
{
    public class GetAllMaturityLevelsByCompanyIdDto
    {
        public int maturityLevelId { get; set; }
        public string name { get; set; }
        public string abbreviation { get; set; }
        public decimal? value { get; set; }
        public string color { get; set; }
    }
}

