using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Qualifier.Application.Database.ControlEvaluation.Commands.CreateControlEvaluation;
using Qualifier.Application.Database.ControlEvaluation.Commands.DeleteControlEvaluation;
using Qualifier.Application.Database.ControlEvaluation.Commands.UpdateControlEvaluation;
using Qualifier.Application.Database.ControlEvaluation.Queries.GetControlEvaluationById;
using Qualifier.Application.Database.ControlEvaluation.Queries.GetControlEvaluationByProcess;

namespace Qualifier.Api.Controllers;

[Route("api/[controller]")]
[Authorize]
public class ControlEvaluationController(
    IGetControlEvaluationByProcessQuery getByProcessQuery,
    IGetControlEvaluationByIdQuery getByIdQuery,
    ICreateControlEvaluationCommand createCommand,
    IUpdateControlEvaluationCommand updateCommand,
    IDeleteControlEvaluationCommand deleteCommand
) : ApiBaseController
{
    [HttpGet("all")]
    public async Task<IActionResult> GetAll(int standardId, int evaluationId, bool scopeToUser = false)
    {
        var res = await getByProcessQuery.Execute(standardId, evaluationId, UserId, scopeToUser);
        return ProcessResponse(res);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var res = await getByIdQuery.Execute(id);
        return ProcessResponse(res, wrapWithData: true);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateControlEvaluationDto model)
    {
        model.creationUserId = UserId;
        model.companyId = CompanyId;

        var res = await createCommand.Execute(model);
        return ProcessResponse(res, wrapWithData: true);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateControlEvaluationDto model)
    {
        model.updateUserId = UserId;
        model.companyId = CompanyId;

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