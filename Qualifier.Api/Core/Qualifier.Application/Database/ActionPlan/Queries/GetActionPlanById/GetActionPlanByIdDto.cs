namespace Qualifier.Application.Database.ActionPlan.Queries.GetActionPlanById
{
    public class GetActionPlanByIdDto
    {
        public int actionPlanId { get; set; }
        public int breachId { get; set; }
        public int evaluationId { get; set; }
        public int standardId { get; set; }
        public string title { get; set; } = string.Empty;
        public string? description { get; set; }
        public int responsibleId { get; set; }
        public DateTime startDate { get; set; }
        public DateTime dueDate { get; set; }
        public int actionPlanStatusId { get; set; }
        public int actionPlanPriorityId { get; set; }

    }
}

