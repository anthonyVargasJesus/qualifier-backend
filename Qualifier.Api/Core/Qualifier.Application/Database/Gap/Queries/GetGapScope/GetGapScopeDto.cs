namespace Qualifier.Application.Database.Gap.Queries.GetGapScope
{
    // Versión liviana de lo que hoy arma /api/requirementEvaluation/all +
    // /api/controlEvaluation/all combinados (ver GapEvaluationData en el
    // frontend): la app "Plan de acción" no necesita nombre/descripción/tema/
    // evidencias de cada ítem, solo saber qué requisitos/controles están en
    // alcance del usuario para poder filtrar qué brechas mostrar
    // (visibleBreaches() en gap_status.dart).
    public class GetGapScopeItemDto
    {
        public string tipo { get; set; } = "";
        public int itemId { get; set; }
    }
}
