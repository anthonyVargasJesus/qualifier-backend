using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Qualifier.Application.Database.Gap.Queries.GetGapScope;

namespace Qualifier.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class GapController(IGetGapScopeQuery getGapScopeQuery) : ApiBaseController
{
    // Versión liviana de requirementEvaluation/all + controlEvaluation/all
    // combinados: solo {tipo, itemId} de los ítems en alcance del usuario,
    // para que "Plan de acción" pueda filtrar brechas sin pedir el árbol
    // completo (nombres, descripciones, madurez, evidencias) que solo usa
    // la pestaña de Evaluación.
    [HttpGet("scope")]
    public async Task<IActionResult> GetScope(int standardId, int evaluationId, bool scopeToUser = true)
    {
        var res = await getGapScopeQuery.Execute(standardId, evaluationId, UserId, scopeToUser);
        return ProcessResponse(res);
    }
}
