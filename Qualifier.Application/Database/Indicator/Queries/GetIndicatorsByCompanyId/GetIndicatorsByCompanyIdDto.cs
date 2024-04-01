namespace Qualifier.Application.Database.Indicator.Queries.GetIndicatorsByCompanyId
{
    public class GetIndicatorsByCompanyIdDto
    {
        public int indicatorId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string abbreviation { get; set; }
        public decimal minimum { get; set; }
        public decimal maximum { get; set; }
        public string color { get; set; }

    }
}

