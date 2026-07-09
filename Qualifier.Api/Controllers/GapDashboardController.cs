using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Qualifier.Application.Database.GapDashboard.Queries.GetGapDashboard;
using Qualifier.Application.Database.GapDashboard.Queries.GetHomeDashboardBootstrap;

namespace Qualifier.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class GapDashboardController(
    IGetGapDashboardQuery getGapDashboardQuery,
    IGetHomeDashboardBootstrapQuery getHomeDashboardBootstrapQuery
) : ApiBaseController
{
    // Resumen para el dashboard de Inicio. scopeToUser=true (igual que /gap/panel) para que
    // el responsable solo vea sus grupos/familias asignadas.
    [HttpGet]
    public async Task<IActionResult> Get(int standardId, int evaluationId, bool scopeToUser = true)
    {
        var res = await getGapDashboardQuery.Execute(standardId, evaluationId, UserId, scopeToUser);
        return ProcessResponse(res, wrapWithData: true);
    }

    // Todo lo que HomeDashboardPage necesita para cargar, en una sola
    // respuesta (dashboard + vista previa de notificaciones) — reemplaza 3
    // llamadas HTTP secuenciales del frontend por una sola.
    [HttpGet("bootstrap")]
    public async Task<IActionResult> GetBootstrap(int notificationsPageSize = 3)
    {
        var res = await getHomeDashboardBootstrapQuery.Execute(UserId, notificationsPageSize);
        return ProcessResponse(res);
    }
}
