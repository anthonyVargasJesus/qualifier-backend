namespace Qualifier.Application.Database.ActionPlan.Queries.GetActionPlanCountsByBreach
{
    public class GetActionPlanCountsByBreachDto
    {
        public List<GetActionPlanCountsByBreachItemDto> counts { get; set; } = new();
    }

    public class GetActionPlanCountsByBreachItemDto
    {
        public int breachId { get; set; }
        public int total { get; set; }
        public int completed { get; set; }
    }
}
