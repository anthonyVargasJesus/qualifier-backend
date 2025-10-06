namespace Qualifier.Application.Database.ActionPlanStatus.Queries.GetAllActionPlanStatussByCompanyId
{
    public class GetAllActionPlanStatussByCompanyIdDto
    {
        public int actionPlanStatusId { get; set; }
        public string name { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;
        public string abbreviation { get; set; } = string.Empty;
        public decimal value { get; set; }
        public string color { get; set; } = string.Empty;

    }
}

