using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Qualifier.Application.Database.GapDashboard.Queries.GetGapDashboard;

namespace Qualifier.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class GapDashboardController(IGetGapDashboardQuery getGapDashboardQuery) : ApiBaseController
{
    // Resumen para el dashboard de Inicio. scopeToUser=true (igual que /gap/panel) para que
    // el responsable solo vea sus grupos/familias asignadas.
    [HttpGet]
    public async Task<IActionResult> Get(int standardId, int evaluationId, bool scopeToUser = true)
    {
        var res = await getGapDashboardQuery.Execute(standardId, evaluationId, UserId, scopeToUser);
        return ProcessResponse(res, wrapWithData: true);
    }
}
