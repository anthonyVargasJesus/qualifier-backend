using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Qualifier.Application.Database.RequirementEvaluation.Commands.CreateRequirementEvaluation;
using Qualifier.Application.Database.RequirementEvaluation.Commands.DeleteRequirementEvaluation;
using Qualifier.Application.Database.RequirementEvaluation.Commands.UpdateRequirementEvaluation;
using Qualifier.Application.Database.RequirementEvaluation.Queries.GetRequirementEvaluationById;
using Qualifier.Application.Database.RequirementEvaluation.Queries.GetRequirementEvaluationByProcess;

namespace Qualifier.Api.Controllers;

[Route("api/[controller]")]
[Authorize]
public class RequirementEvaluationController(
    IGetRequirementEvaluationByProcessQuery getByProcessQuery,
    IGetRequirementEvaluationByIdQuery getByIdQuery,
    ICreateRequirementEvaluationCommand createCommand,
    IUpdateRequirementEvaluationCommand updateCommand,
    IDeleteRequirementEvaluationCommand deleteCommand
) : ApiBaseController
{
    [HttpGet("all")]
    public async Task<IActionResult> GetAll(int standardId, int evaluationId, string? search, bool scopeToUser = false)
    {
        var res = await getByProcessQuery.Execute(standardId, evaluationId, search ?? string.Empty, UserId, scopeToUser);
        return ProcessResponse(res);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var res = await getByIdQuery.Execute(id);
        return ProcessResponse(res, wrapWithData: true);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRequirementEvaluationDto model)
    {
        model.creationUserId = UserId;
        model.companyId = CompanyId;

        var res = await createCommand.Execute(model);
        return ProcessResponse(res, wrapWithData: true);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateRequirementEvaluationDto model)
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