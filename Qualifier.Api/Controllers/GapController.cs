using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Qualifier.Application.Database.Gap.Queries.GetPlanDeAccionBootstrap;

namespace Qualifier.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class GapController(IGetPlanDeAccionBootstrapQuery getPlanDeAccionBootstrapQuery) : ApiBaseController
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
}
