namespace Qualifier.Application.Database.Breach.Queries.GetBreachesScope
{
    // Versión liviana de GetBreachsByEvaluationIdDto: "Plan de acción" solo
    // necesita estos 6 campos (ya están directo en la fila de MAE_BREACH) para
    // armar sus tarjetas — no el join a severity/status/control/requirement/
    // responsible ni el resumen HTML de acciones que arma GetBreachsByEvaluationIdQuery.
    public class GetBreachesScopeDto
    {
        public int breachId { get; set; }
        public string type { get; set; } = "";
        public int requirementId { get; set; }
        public int controlId { get; set; }
        public string numerationToShow { get; set; } = "";
        public string title { get; set; } = "";
    }
}
