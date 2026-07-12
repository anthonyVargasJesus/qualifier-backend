namespace Qualifier.Application.Database.Breach.Queries.GetBreachesWithoutActionPlan
{
    public class GetBreachesWithoutActionPlanDto
    {
        public bool hasCurrentEvaluation { get; set; }
        public int? evaluationId { get; set; }
        public int totalBreaches { get; set; }
        public int withoutActionPlanCount { get; set; }
        public List<GetBreachWithoutActionPlanItemDto> items { get; set; } = new();
    }

    public class GetBreachWithoutActionPlanItemDto
    {
        public int breachId { get; set; }
        public string title { get; set; } = string.Empty;
        public string? numerationToShow { get; set; }
        public string severityName { get; set; } = string.Empty;
        public string severityColor { get; set; } = string.Empty;
        public string responsibleName { get; set; } = string.Empty;
    }
}
