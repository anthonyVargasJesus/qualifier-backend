namespace Qualifier.Application.Database.Evaluation.Queries.GetComplianceEvolution
{
    public class GetComplianceEvolutionDto
    {
        public int? standardId { get; set; }
        public string? standardName { get; set; }
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

        // "¿En qué mejoramos/empeoramos?" — ítems cuyo nivel de madurez cambió entre la
        // evaluación anterior y la actual (ver GetComplianceEvolutionQuery). Sin esto, el
        // reporte solo decía "subió 4.2%" sin decir en qué — la pregunta obvia que sigue.
        public int improvedCount { get; set; }
        public int worsenedCount { get; set; }
        public List<GetComplianceEvolutionChangeDto> changes { get; set; } = new();

        // Composición por nivel de madurez de la evaluación actual (Cumple/Parcial/No
        // cumple/No aplica) — el % agregado de arriba no dice cómo se ve la mezcla real
        // detrás de ese número. Mismos colores que ya usa el catálogo de niveles de
        // madurez en el resto de la app (gap-home, etc.), no una paleta nueva.
        public List<GetComplianceEvolutionCompositionDto> requirementsComposition { get; set; } = new();
        public List<GetComplianceEvolutionCompositionDto> controlsComposition { get; set; } = new();
    }

    public class GetComplianceEvolutionCompositionDto
    {
        public string name { get; set; } = string.Empty;
        public string? color { get; set; }
        public int count { get; set; }
        public decimal percentage { get; set; }
    }

    public class GetComplianceEvolutionChangeDto
    {
        public string tipo { get; set; } = string.Empty; // "requisito" | "control"
        public string code { get; set; } = string.Empty;
        public string name { get; set; } = string.Empty;
        public string? previousLevel { get; set; }
        public string? currentLevel { get; set; }
        public bool improved { get; set; }
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
