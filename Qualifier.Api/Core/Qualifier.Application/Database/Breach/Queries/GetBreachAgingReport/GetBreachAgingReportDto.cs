namespace Qualifier.Application.Database.Breach.Queries.GetBreachAgingReport
{
    public class GetBreachAgingReportDto
    {
        public bool hasCurrentEvaluation { get; set; }
        public int? evaluationId { get; set; }
        public List<GetBreachAgingBucketDto> buckets { get; set; } = new();
    }

    public class GetBreachAgingBucketDto
    {
        public string label { get; set; } = string.Empty;
        public int count { get; set; }
        public List<GetBreachAgingItemDto> breaches { get; set; } = new();
    }

    public class GetBreachAgingItemDto
    {
        public int breachId { get; set; }
        public string title { get; set; } = string.Empty;
        public string? numerationToShow { get; set; }
        public int daysOpen { get; set; }
        public string severityName { get; set; } = string.Empty;
        public string severityColor { get; set; } = string.Empty;
    }
}
