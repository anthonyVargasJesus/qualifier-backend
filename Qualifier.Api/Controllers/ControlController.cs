using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Qualifier.Application.Database.Control.Commands.CreateControl;
using Qualifier.Application.Database.Control.Commands.DeleteControl;
using Qualifier.Application.Database.Control.Commands.UpdateControl;
using Qualifier.Application.Database.Control.Queries.GetAllControlsByStandardId;
using Qualifier.Application.Database.Control.Queries.GetControlById;
using Qualifier.Application.Database.Control.Queries.GetControlsByControlGroupId;

namespace Qualifier.Api.Controllers;

[Route("api/[controller]")]
[Authorize]
public class ControlController(
                                IGetAllControlsByStandardIdQuery getAllQuery,
                                IGetControlsByControlGroupIdQuery getPagedQuery,
                                IGetControlByIdQuery getByIdQuery,
                                ICreateControlCommand createCommand,
                                IUpdateControlCommand updateCommand,
                                IDeleteControlCommand deleteCommand
                              ) : ApiBaseController
{
    [HttpGet("all")]
    public async Task<IActionResult> GetAll(int standardId)
    {
        var res = await getAllQuery.Execute(standardId);
        return ProcessResponse(res);
    }

    [HttpGet]
    public async Task<IActionResult> Get(int skip, int pageSize, string? search, int controlGroupId)
    {
        var res = await getPagedQuery.Execute(skip, pageSize, search ?? string.Empty, controlGroupId);
        return ProcessResponse(res);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var res = await getByIdQuery.Execute(id);
        return ProcessResponse(res, wrapWithData: true);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateControlDto model)
    {
        model.creationUserId = UserId;
        model.companyId = CompanyId;

        var res = await createCommand.Execute(model);
        return ProcessResponse(res, wrapWithData: true);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateControlDto model)
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