using System.ComponentModel.Design;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Qualifier.Api.Controllers;
using Qualifier.Application.Database.ActionPlanStatus.Commands.CreateActionPlanStatus;
using Qualifier.Application.Database.ActionPlanStatus.Commands.DeleteActionPlanStatus;
using Qualifier.Application.Database.ActionPlanStatus.Commands.UpdateActionPlanStatus;
using Qualifier.Application.Database.ActionPlanStatus.Queries.GetActionPlanStatusById;
using Qualifier.Application.Database.ActionPlanStatus.Queries.GetActionPlanStatussByCompanyId;
using Qualifier.Application.Database.ActionPlanStatus.Queries.GetAllActionPlanStatussByCompanyId;

namespace qualifier.Api.Controllers;

[Route("api/[controller]")]
[Authorize]
public class ActionPlanStatusController(
    IGetAllActionPlanStatussByCompanyIdQuery getAllQuery,
    IGetActionPlanStatussByCompanyIdQuery getPagedQuery,
    IGetActionPlanStatusByIdQuery getByIdQuery,
    ICreateActionPlanStatusCommand createCommand,
    IUpdateActionPlanStatusCommand updateCommand,
    IDeleteActionPlanStatusCommand deleteCommand
) : ApiBaseController
{
    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        if (CompanyId == 0) return CompanyRequiredError();

        var res = await getAllQuery.Execute(CompanyId);
        return ProcessResponse(res);
    }

    [HttpGet]
    public async Task<IActionResult> Get(int skip, int pageSize, string? search)
    {
        if (CompanyId == 0) return CompanyRequiredError();

        var res = await getPagedQuery.Execute(skip, pageSize, search ?? string.Empty, CompanyId);
        return ProcessResponse(res);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var res = await getByIdQuery.Execute(id);
        return ProcessResponse(res, wrapWithData: true);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateActionPlanStatusDto model)
    {
        model.creationUserId = UserId;
        model.companyId = CompanyId;

        var res = await createCommand.Execute(model);
        return ProcessResponse(res, wrapWithData: true);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateActionPlanStatusDto model)
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