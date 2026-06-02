namespace Qualifier.Application.Database.ActionPlan.Queries.GetActionPlanProgress
{
    public class GetActionPlanProgressDto
    {
        public bool hasCurrentEvaluation { get; set; }
        public int? evaluationId { get; set; }
        public string? evaluationDescription { get; set; }
        public DateTime? evaluationStartDate { get; set; }
        public DateTime? evaluationEndDate { get; set; }
        public int totalPlans { get; set; }
        public int totalResponsibles { get; set; }
        public int overdueCount { get; set; }
        public decimal globalCompletionRate { get; set; }
        public List<GetActionPlanProgressResponsibleDto> responsibles { get; set; } = new();
        public List<GetActionPlanProgressPieDto> statusChart { get; set; } = new();
        public List<GetActionPlanProgressPieDto> priorityChart { get; set; } = new();
        public List<GetActionPlanProgressStackedDto> responsibleStackedChart { get; set; } = new();
        public List<string> statusColors { get; set; } = new();
        public List<string> priorityColors { get; set; } = new();
    }

    public class GetActionPlanProgressResponsibleDto
    {
        public int responsibleId { get; set; }
        public string responsibleName { get; set; } = string.Empty;
        public int total { get; set; }
        public int completed { get; set; }
        public int overdue { get; set; }
        public decimal completionRate { get; set; }
        public List<GetActionPlanProgressStatusDetailDto> statusBreakdown { get; set; } = new();
    }

    public class GetActionPlanProgressStatusDetailDto
    {
        public string statusName { get; set; } = string.Empty;
        public string abbreviation { get; set; } = string.Empty;
        public string color { get; set; } = string.Empty;
        public int count { get; set; }
    }

    public class GetActionPlanProgressPieDto
    {
        public string name { get; set; } = string.Empty;
        public int value { get; set; }
    }

    public class GetActionPlanProgressStackedDto
    {
        public string name { get; set; } = string.Empty;
        public List<GetActionPlanProgressPieDto> series { get; set; } = new();
    }
}
