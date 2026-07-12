namespace Qualifier.Application.Database.ActionPlan.Queries.GetOverdueActionPlansReport
{
    public class GetOverdueActionPlansReportDto
    {
        public bool hasCurrentEvaluation { get; set; }
        public int? evaluationId { get; set; }
        public List<GetOverdueActionPlansBucketDto> buckets { get; set; } = new();
    }

    public class GetOverdueActionPlansBucketDto
    {
        public string label { get; set; } = string.Empty;
        public int count { get; set; }
        public List<GetOverdueActionPlanItemDto> items { get; set; } = new();
    }

    public class GetOverdueActionPlanItemDto
    {
        public int actionPlanId { get; set; }
        public string title { get; set; } = string.Empty;
        public string breachTitle { get; set; } = string.Empty;
        public string? breachNumerationToShow { get; set; }
        public string responsibleName { get; set; } = string.Empty;
        public int daysOverdue { get; set; }
    }
}
