using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Qualifier.Application.Database.ActionPlan.Commands.CreateActionPlan;
using Qualifier.Application.Database.ActionPlan.Commands.DeleteActionPlan;
using Qualifier.Application.Database.ActionPlan.Commands.UpdateActionPlan;
using Qualifier.Application.Database.ActionPlan.Queries.GetActionPlanById;
using Qualifier.Application.Database.ActionPlan.Queries.GetActionPlanProgress;
using Qualifier.Application.Database.ActionPlan.Queries.GetActionPlansByBreachId;
using Qualifier.Application.Database.ActionPlan.Queries.GetActionPlansByUserId;
using Qualifier.Application.Database.ActionPlan.Queries.GetMyActionsBootstrap;

namespace Qualifier.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ActionPlanController(
    IGetActionPlansByBreachIdQuery getPagedQuery,
    IGetActionPlanByIdQuery getByIdQuery,
    IGetActionPlanProgressQuery getProgressQuery,
    IGetActionPlansByUserIdQuery getByUserQuery,
    IGetMyActionsBootstrapQuery getMyActionsBootstrapQuery,
    ICreateActionPlanCommand createCommand,
    IUpdateActionPlanCommand updateCommand,
    IDeleteActionPlanCommand deleteCommand
) : ApiBaseController
{
    [HttpGet("progress")]
    public async Task<IActionResult> GetProgress()
    {
        if (CompanyId == 0) return CompanyRequiredError();

        var res = await getProgressQuery.Execute(CompanyId);
        return ProcessResponse(res);
    }

    // Tareas asignadas al usuario logeado (JWT) para la evaluación indicada — no recibe userId
    // del cliente, siempre son "mis" acciones, no las de otro usuario.
    [HttpGet("byUser")]
    public async Task<IActionResult> GetByUser(int evaluationId)
    {
        var res = await getByUserQuery.Execute(UserId, evaluationId);
        return ProcessResponse(res);
    }

    // Todo lo que "Mis acciones" necesita para cargar, en una sola respuesta
    // (evaluación actual resuelta server-side + tareas + estados) — reemplaza
    // 3 llamadas HTTP secuenciales del frontend por una sola.
    [HttpGet("my-actions-bootstrap")]
    public async Task<IActionResult> GetMyActionsBootstrap()
    {
        var res = await getMyActionsBootstrapQuery.Execute(CompanyId, UserId);
        return ProcessResponse(res);
    }

    [HttpGet]
    public async Task<IActionResult> Get(int skip, int pageSize, string? search, int breachId)
    {
        if (CompanyId == 0) return CompanyRequiredError();

        var res = await getPagedQuery.Execute(skip, pageSize, search ?? string.Empty, breachId);
        return ProcessResponse(res);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var res = await getByIdQuery.Execute(id);
        return ProcessResponse(res, wrapWithData: true);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateActionPlanDto model)
    {
        model.creationUserId = UserId;
        model.companyId = CompanyId;

        var res = await createCommand.Execute(model);
        return ProcessResponse(res, wrapWithData: true);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateActionPlanDto model)
    {
        model.updateUserId = UserId;

        var res = await updateCommand.Execute(model, id);
        return ProcessResponse(res, wrapWithData: true);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var res = await deleteCommand.Execute(id, UserId);
        return ProcessResponse(res, wrapWithData: true);
    }
}