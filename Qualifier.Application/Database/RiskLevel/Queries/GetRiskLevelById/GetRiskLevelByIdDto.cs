namespace Qualifier.Application.Database.RiskLevel.Queries.GetRiskLevelById
{
    public class GetRiskLevelByIdDto
    {
        public int riskLevelId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string abbreviation { get; set; }
        public decimal? factor { get; set; }
        public decimal minimum { get; set; }
        public decimal? maximum { get; set; }
        public string color { get; set; }

    }
}

