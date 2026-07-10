using Qualifier.Application.Database.ActionPlanPriority.Queries.GetAllActionPlanPrioritiesByCompanyId;
using Qualifier.Application.Database.ActionPlanStatus.Queries.GetAllActionPlanStatussByCompanyId;
using Qualifier.Application.Database.Breach.Queries.GetBreachesScope;
using Qualifier.Application.Database.Evaluation.Queries.GetCurrentEvaluation;
using Qualifier.Application.Database.MaturityLevel.Queries.GetAllMaturityLevelsByCompanyId;
using Qualifier.Application.Database.User.Queries.GetAllUsersByCompanyId;

namespace Qualifier.Application.Database.Gap.Queries.GetPlanDeAccionBootstrap
{
    // Todo lo que "Plan de acción" necesita para cargar, en una sola
    // respuesta — reemplaza 7 llamadas HTTP secuenciales del frontend
    // (evaluación, niveles de madurez, alcance de ítems, brechas, estados/
    // prioridades/usuarios de acción) por una sola. Reusa los DTOs que cada
    // query ya devolvía por separado, así que el frontend parsea cada
    // sección igual que parseaba la respuesta individual de antes.
    public class GetPlanDeAccionBootstrapDto
    {
        public GetCurrentEvaluationDto evaluation { get; set; } = null!;
        public List<GetGapScopeItemDto> items { get; set; } = new();
        public List<GetAllMaturityLevelsByCompanyIdDto> maturityLevels { get; set; } = new();
        public List<GetBreachesScopeDto> breaches { get; set; } = new();
        public List<GetAllActionPlanStatussByCompanyIdDto> actionPlanStatuses { get; set; } = new();
        public List<GetAllActionPlanPrioritiesByCompanyIdDto> actionPlanPriorities { get; set; } = new();
        public List<GetAllUsersByCompanyIdDto> users { get; set; } = new();
    }

    // Movida tal cual desde GetGapScope/GetGapScopeDto.cs (ese endpoint se
    // eliminó porque nada más lo usaba salvo Plan de acción).
    public class GetGapScopeItemDto
    {
        public string tipo { get; set; } = "";
        public int itemId { get; set; }
        public int? maturityLevelId { get; set; }
        // "Cláusulas" para requisitos, o el nombre del grupo de control —
        // mismo criterio que ya usa Evaluación, para poder agrupar "Plan de
        // acción" por tema sin tener que traer nombre/código/descripción
        // completos de cada ítem (eso seguiría siendo pesado a propósito).
        public string theme { get; set; } = "";
    }
}
