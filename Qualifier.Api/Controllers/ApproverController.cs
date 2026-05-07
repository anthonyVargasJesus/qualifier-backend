using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Qualifier.Application.Database.Approver.Commands.CreateApprover;
using Qualifier.Application.Database.Approver.Commands.DeleteApprover;
using Qualifier.Application.Database.Approver.Commands.UpdateApprover;
using Qualifier.Application.Database.Approver.Queries.GetAllApproversByVersionId;
using Qualifier.Application.Database.Approver.Queries.GetApproverById;

namespace Qualifier.Api.Controllers;

[Route("api/[controller]")]
[Authorize]
public class ApproverController(
    IGetAllApproversByVersionIdQuery getAllQuery,
    IGetApproverByIdQuery getByIdQuery,
    ICreateApproverCommand createCommand,
    IUpdateApproverCommand updateCommand,
    IDeleteApproverCommand deleteCommand
) : ApiBaseController
{
    [HttpGet("all")]
    public async Task<IActionResult> GetAll(int versionId)
    {
        var res = await getAllQuery.Execute(versionId);
        return ProcessResponse(res);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var res = await getByIdQuery.Execute(id);
        return ProcessResponse(res, wrapWithData: true);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateApproverDto model)
    {
        model.creationUserId = UserId;
        model.companyId = CompanyId;

        var res = await createCommand.Execute(model);
        return ProcessResponse(res, wrapWithData: true);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateApproverDto model)
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