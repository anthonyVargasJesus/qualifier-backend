namespace Qualifier.Application.Database.ActionPlan.Queries.GetActionPlansByBreachId
{
    public class GetActionPlansByBreachIdDto
    {
        public int actionPlanId { get; set; }
        public int breachId { get; set; }
        public int evaluationId { get; set; }
        public int standardId { get; set; }
        public string title { get; set; } = string.Empty;
        public string? description { get; set; }
        public int? userId { get; set; }
        public DateTime startDate { get; set; }
        public DateTime dueDate { get; set; }
        public int actionPlanStatusId { get; set; }
        public int actionPlanPriorityId { get; set; }
        public GetActionPlansByBreachIdActionPlanPriorityDto? actionPlanPriority { get; set; }
        public GetActionPlansByBreachIdActionPlanStatusDto? actionPlanStatus { get; set; }
        public GetActionPlansByBreachIdUserDto? user { get; set; }
    }
    public class GetActionPlansByBreachIdActionPlanPriorityDto
    {
        public string name { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;
        public string color { get; set; } = string.Empty;

    }
    public class GetActionPlansByBreachIdActionPlanStatusDto
    {
        public string name { get; set; } = string.Empty;
        public string abbreviation { get; set; } = string.Empty;
        public string color { get; set; } = string.Empty;

    }
    public class GetActionPlansByBreachIdUserDto
    {
        public string name { get; set; } = string.Empty;

    }
}

