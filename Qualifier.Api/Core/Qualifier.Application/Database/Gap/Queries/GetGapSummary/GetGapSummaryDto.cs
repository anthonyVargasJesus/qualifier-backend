using Qualifier.Application.Database.Evaluation.Queries.GetCurrentEvaluation;
using Qualifier.Application.Database.MaturityLevel.Queries.GetAllMaturityLevelsByCompanyId;
using Qualifier.Application.Database.Responsible.Queries.GetAllResponsiblesByStandardId;

namespace Qualifier.Application.Database.Gap.Queries.GetGapSummary
{
    public class GetGapSummaryDto
    {
        public GetCurrentEvaluationDto evaluation { get; set; } = null!;
        public List<GetAllMaturityLevelsByCompanyIdDto> maturityLevels { get; set; } = new();
        public List<GetAllResponsiblesByStandardIdDto> responsibles { get; set; } = new();
        public List<string> temas { get; set; } = new();
        public GetGapSummaryStatsDto stats { get; set; } = new();
        public List<GetGapSummaryThemeRowDto> temaRows { get; set; } = new();
        // Conteo global por nivel de madurez ("Cumple (21) Parcial (118) ... Pendiente (1)"),
        // el mismo que hoy calcula countEstado() en gap-home.component.ts sobre this.items.
        public List<GetGapSummarySegmentDto> legend { get; set; } = new();
        public int evidencias { get; set; }
    }

    public class GetGapSummaryStatsDto
    {
        public int pct { get; set; }
        public int evaluados { get; set; }
        public int total { get; set; }
    }

    public class GetGapSummaryThemeRowDto
    {
        public string theme { get; set; } = string.Empty;
        public int total { get; set; }
        public int evaluados { get; set; }
        public int pendiente { get; set; }
        public List<GetGapSummarySegmentDto> segments { get; set; } = new();
    }

    public class GetGapSummarySegmentDto
    {
        public string name { get; set; } = string.Empty;
        public string color { get; set; } = string.Empty;
        public int count { get; set; }
    }
}
