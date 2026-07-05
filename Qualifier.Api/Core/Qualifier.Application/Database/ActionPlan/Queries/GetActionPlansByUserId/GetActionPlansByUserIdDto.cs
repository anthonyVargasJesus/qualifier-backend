namespace Qualifier.Application.Database.ActionPlan.Queries.GetActionPlansByUserId
{
    public class GetActionPlansByUserIdDto
    {
        public int actionPlanId { get; set; }
        public int breachId { get; set; }
        public int evaluationId { get; set; }
        public int standardId { get; set; }
        public int userId { get; set; }
        public string breachNumerationToShow { get; set; } = string.Empty;
        public string breachTitle { get; set; } = string.Empty;
        public string title { get; set; } = string.Empty;
        public string? description { get; set; }
        public DateTime startDate { get; set; }
        public DateTime dueDate { get; set; }
        public int actionPlanStatusId { get; set; }
        public string statusName { get; set; } = string.Empty;
        public string statusColor { get; set; } = string.Empty;
        public int actionPlanPriorityId { get; set; }
        public string priorityName { get; set; } = string.Empty;
        public string priorityColor { get; set; } = string.Empty;
    }
}
