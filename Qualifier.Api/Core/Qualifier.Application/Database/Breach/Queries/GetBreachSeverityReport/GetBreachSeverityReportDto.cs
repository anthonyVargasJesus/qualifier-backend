namespace Qualifier.Application.Database.Breach.Queries.GetBreachSeverityReport
{
    public class GetBreachSeverityReportDto
    {
        public bool hasCurrentEvaluation { get; set; }
        public int? evaluationId { get; set; }
        public string? evaluationDescription { get; set; }
        public DateTime? evaluationStartDate { get; set; }
        public DateTime? evaluationEndDate { get; set; }
        public int totalBreaches { get; set; }
        public int withoutActionPlan { get; set; }
        public int highestSeverityCount { get; set; }
        public string? highestSeverityName { get; set; }
        public string? highestSeverityColor { get; set; }
        public int totalResponsibles { get; set; }
        public List<GetBreachSeverityReportBreachDto> breaches { get; set; } = new();
        public List<GetBreachSeverityReportPieDto> severityChart { get; set; } = new();
        public List<GetBreachSeverityReportPieDto> statusChart { get; set; } = new();
        public List<string> severityColors { get; set; } = new();
        public List<string> statusColors { get; set; } = new();
    }

    public class GetBreachSeverityReportBreachDto
    {
        public int breachId { get; set; }
        public string title { get; set; } = string.Empty;
        public string? numerationToShow { get; set; }
        public string? type { get; set; }
        public string severityName { get; set; } = string.Empty;
        public string severityAbbreviation { get; set; } = string.Empty;
        public string severityColor { get; set; } = string.Empty;
        public decimal severityValue { get; set; }
        public string statusName { get; set; } = string.Empty;
        public string statusAbbreviation { get; set; } = string.Empty;
        public string statusColor { get; set; } = string.Empty;
        public string responsibleName { get; set; } = string.Empty;
        public int actionPlanCount { get; set; }
    }

    public class GetBreachSeverityReportPieDto
    {
        public string name { get; set; } = string.Empty;
        public int value { get; set; }
    }
}
