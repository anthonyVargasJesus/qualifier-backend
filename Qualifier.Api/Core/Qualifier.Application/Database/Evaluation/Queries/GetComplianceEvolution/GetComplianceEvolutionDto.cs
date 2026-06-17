namespace Qualifier.Application.Database.Evaluation.Queries.GetComplianceEvolution
{
    public class GetComplianceEvolutionDto
    {
        public int totalEvaluations { get; set; }
        public decimal? currentRequirementsRate { get; set; }
        public decimal? currentControlsRate { get; set; }
        public decimal? currentOverallRate { get; set; }
        public decimal? deltaRequirements { get; set; }
        public decimal? deltaControls { get; set; }
        public string? currentEvaluationDescription { get; set; }
        public string? previousEvaluationDescription { get; set; }
        public List<GetComplianceEvolutionItemDto> evaluations { get; set; } = new();
        public List<GetComplianceEvolutionLineDto> chart { get; set; } = new();
    }

    public class GetComplianceEvolutionItemDto
    {
        public int evaluationId { get; set; }
        public string description { get; set; } = string.Empty;
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public bool isCurrent { get; set; }
        public decimal? requirementsRate { get; set; }
        public decimal? controlsRate { get; set; }
        public decimal? overallRate { get; set; }
        public int requirementsCount { get; set; }
        public int controlsCount { get; set; }
    }

    public class GetComplianceEvolutionLineDto
    {
        public string name { get; set; } = string.Empty;
        public List<GetComplianceEvolutionSeriesDto> series { get; set; } = new();
    }

    public class GetComplianceEvolutionSeriesDto
    {
        public string name { get; set; } = string.Empty;
        public decimal value { get; set; }
    }
}
