using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Qualifier.Application.Database.Requirement.Commands.CreateRequirement;
using Qualifier.Application.Database.Requirement.Commands.DeleteRequirement;
using Qualifier.Application.Database.Requirement.Commands.UpdateRequirement;
using Qualifier.Application.Database.Requirement.Queries.GetAllRequirementsByStandardId;
using Qualifier.Application.Database.Requirement.Queries.GetRequirementById;
using Qualifier.Application.Database.Requirement.Queries.GetRequirementsByStandardId;

namespace Qualifier.Api.Controllers;

[Route("api/[controller]")]
[Authorize]
public class RequirementController(
                                    IGetAllRequirementsByStandardIdQuery getAllQuery,
                                    IGetRequirementsByStandardIdQuery getPagedQuery,
                                    IGetRequirementByIdQuery getByIdQuery,
                                    ICreateRequirementCommand createCommand,
                                    IUpdateRequirementCommand updateCommand,
                                    IDeleteRequirementCommand deleteCommand
                                  ) : ApiBaseController
{
    [HttpGet("all")]
    public async Task<IActionResult> GetAll(int standardId)
    {
        var res = await getAllQuery.Execute(standardId);
        return ProcessResponse(res);
    }

    [HttpGet]
    public async Task<IActionResult> Get(int standardId)
    {
        var res = await getPagedQuery.Execute(standardId);
        return ProcessResponse(res);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var res = await getByIdQuery.Execute(id);
        return ProcessResponse(res, wrapWithData: true);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRequirementDto model)
    {
        model.creationUserId = UserId;
        model.companyId = CompanyId;

        var res = await createCommand.Execute(model);
        return ProcessResponse(res, wrapWithData: true);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateRequirementDto model)
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