using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Qualifier.Application.Database.Gap.Queries.GetEvaluacionBootstrap;
using Qualifier.Application.Database.Gap.Queries.GetGapItems;
using Qualifier.Application.Database.Gap.Queries.GetGapSummary;
using Qualifier.Application.Database.Gap.Queries.GetPlanDeAccionBootstrap;

namespace Qualifier.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class GapController(
    IGetPlanDeAccionBootstrapQuery getPlanDeAccionBootstrapQuery,
    IGetEvaluacionBootstrapQuery getEvaluacionBootstrapQuery,
    IGetGapSummaryQuery getGapSummaryQuery,
    IGetGapItemsQuery getGapItemsQuery
) : ApiBaseController
{
    // Todo lo que "Plan de acción" necesita para cargar, en una sola
    // respuesta (evaluación, niveles de madurez, alcance de ítems, brechas,
    // estados/prioridades/usuarios de acción) — reemplaza 7 llamadas HTTP
    // secuenciales del frontend por una sola. standardId/evaluationId no se
    // piden como query params: se resuelven del lado del servidor (standardId
    // del JWT, evaluationId de la evaluación marcada como actual).
    [HttpGet("plan-de-accion")]
    public async Task<IActionResult> GetPlanDeAccionBootstrap(bool scopeToUser = true)
    {
        var res = await getPlanDeAccionBootstrapQuery.Execute(CompanyId, UserId, StandardId, scopeToUser);
        return ProcessResponse(res);
    }

    // Todo lo que la pestaña "Evaluación" necesita para cargar, en una sola
    // respuesta — reemplaza 4 llamadas HTTP secuenciales del frontend por
    // una sola. El reporte SOA reutiliza la misma pantalla/provider, así que
    // se beneficia del mismo ahorro sin pedir nada aparte.
    [HttpGet("evaluacion")]
    public async Task<IActionResult> GetEvaluacionBootstrap()
    {
        var res = await getEvaluacionBootstrapQuery.Execute(CompanyId, UserId);
        return ProcessResponse(res);
    }

    // Header (%, mapa de brecha por tema, leyenda) de "Análisis del GAP" — números globales,
    // no dependen de la búsqueda/filtro/página actual de /api/gap/items. Se pide una vez al
    // cargar la pantalla y de nuevo cuando se guarda un ítem (mismo momento en que antes se
    // volvía a pedir /api/gap/evaluacion completo).
    [HttpGet("resumen")]
    public async Task<IActionResult> GetResumen(bool scopeToUser = true)
    {
        var res = await getGapSummaryQuery.Execute(CompanyId, UserId, scopeToUser);
        return ProcessResponse(res);
    }

    // Lista paginada de ítems evaluables (requisitos + controles) de la evaluación actual —
    // reemplaza el árbol completo de /api/gap/evaluacion para el scroll infinito de gap-home.
    [HttpGet("items")]
    public async Task<IActionResult> GetItems(int skip, int pageSize, string? search, string? theme, bool scopeToUser = true)
    {
        var res = await getGapItemsQuery.Execute(UserId, scopeToUser, skip, pageSize, search ?? string.Empty, theme ?? string.Empty);
        return ProcessResponse(res);
    }
}
