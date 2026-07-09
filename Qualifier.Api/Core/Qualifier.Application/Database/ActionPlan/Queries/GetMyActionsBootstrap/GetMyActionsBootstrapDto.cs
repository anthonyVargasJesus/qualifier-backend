using Qualifier.Application.Database.ActionPlan.Queries.GetActionPlansByUserId;
using Qualifier.Application.Database.ActionPlanStatus.Queries.GetAllActionPlanStatussByCompanyId;

namespace Qualifier.Application.Database.ActionPlan.Queries.GetMyActionsBootstrap
{
    // Todo lo que "Mis acciones" necesita para cargar, en una sola respuesta
    // — reemplaza 3 llamadas HTTP secuenciales del frontend (evaluación
    // actual, tareas del usuario, estados) por una sola. La evaluación
    // actual se resuelve del lado del servidor solo para poder pedir las
    // tareas del usuario; el frontend nunca necesitó el objeto evaluación en
    // sí (cada tarea ya trae su propio evaluationId).
    public class GetMyActionsBootstrapDto
    {
        public List<GetActionPlansByUserIdDto> tasks { get; set; } = new();
        public List<GetAllActionPlanStatussByCompanyIdDto> statuses { get; set; } = new();
    }
}
