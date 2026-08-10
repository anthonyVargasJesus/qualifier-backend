namespace Qualifier.Application.Database.Gap.Queries.GetGapItems
{
    public class GetGapItemsDto
    {
        public string tipo { get; set; } = string.Empty; // "requisito" | "control"
        public int itemId { get; set; }
        // requirementEvaluationId / controlEvaluationId real (null si el ítem nunca se evaluó
        // para esta evaluación) — lo que app-gap-evaluation-item necesita para guardar y para
        // pedirle evidencia a gap-evidence-list.
        public long? evaluationItemId { get; set; }
        public string code { get; set; } = string.Empty;
        public string name { get; set; } = string.Empty;
        public string theme { get; set; } = string.Empty;
        public int? maturityLevelId { get; set; }
        public decimal? value { get; set; }
        public string? justification { get; set; }
        public string? improvementActions { get; set; }
        public int? responsibleId { get; set; }
        public bool hasEvidence { get; set; }
    }
}
