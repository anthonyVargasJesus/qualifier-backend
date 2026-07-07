namespace Qualifier.Application.Database.Gap.Queries.GetGapScope
{
    // Versión liviana de lo que hoy arma /api/requirementEvaluation/all +
    // /api/controlEvaluation/all combinados (ver GapEvaluationData en el
    // frontend): la app "Plan de acción" no necesita nombre/descripción/tema/
    // evidencias de cada ítem, solo tipo+itemId (para filtrar qué brechas
    // mostrar, ver visibleBreaches() en gap_status.dart) y maturityLevelId
    // (para el badge de estado — "No cumple"/"Parcial" — de cada brecha, ver
    // estadoDeBreach()/colorDeEstadoBreach() en el mismo archivo).
    public class GetGapScopeItemDto
    {
        public string tipo { get; set; } = "";
        public int itemId { get; set; }
        public int? maturityLevelId { get; set; }
    }
}
