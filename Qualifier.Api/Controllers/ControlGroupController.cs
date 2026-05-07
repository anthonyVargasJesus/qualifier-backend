using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Qualifier.Application.Database.ControlGroup.Commands.CreateControlGroup;
using Qualifier.Application.Database.ControlGroup.Commands.DeleteControlGroup;
using Qualifier.Application.Database.ControlGroup.Commands.UpdateControlGroup;
using Qualifier.Application.Database.ControlGroup.Queries.GetAllControlGroupsByStandardId;
using Qualifier.Application.Database.ControlGroup.Queries.GetControlGroupById;
using Qualifier.Application.Database.ControlGroup.Queries.GetControlGroupsByStandardId;

namespace Qualifier.Api.Controllers;

[Route("api/[controller]")]
[Authorize]
public class ControlGroupController(
    IGetAllControlGroupsByStandardIdQuery getAllQuery,
    IGetControlGroupsByStandardIdQuery getPagedQuery,
    IGetControlGroupByIdQuery getByIdQuery,
    ICreateControlGroupCommand createCommand,
    IUpdateControlGroupCommand updateCommand,
    IDeleteControlGroupCommand deleteCommand
) : ApiBaseController
{
    [HttpGet("all")]
    public async Task<IActionResult> GetAll(int standardId)
    {
        var res = await getAllQuery.Execute(standardId);
        return ProcessResponse(res);
    }

    [HttpGet]
    public async Task<IActionResult> Get(int skip, int pageSize, string? search, int standardId)
    {
        var res = await getPagedQuery.Execute(skip, pageSize, search ?? string.Empty, standardId);
        return ProcessResponse(res);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var res = await getByIdQuery.Execute(id);
        return ProcessResponse(res, wrapWithData: true);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateControlGroupDto model)
    {
        model.creationUserId = UserId;
        model.companyId = CompanyId;

        var res = await createCommand.Execute(model);
        return ProcessResponse(res, wrapWithData: true);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateControlGroupDto model)
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