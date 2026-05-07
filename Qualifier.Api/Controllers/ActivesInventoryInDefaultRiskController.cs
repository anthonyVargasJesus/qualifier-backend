using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Qualifier.Application.Database.ActivesInventoryInDefaultRisk.Commands.CreateActivesInventoryInDefaultRisk;
using Qualifier.Application.Database.ActivesInventoryInDefaultRisk.Commands.DeleteActivesInventoryInDefaultRisk;
using Qualifier.Application.Database.ActivesInventoryInDefaultRisk.Commands.UpdateActivesInventoryInDefaultRisk;
using Qualifier.Application.Database.ActivesInventoryInDefaultRisk.Queries.GetActivesInventoryInDefaultRiskById;
using Qualifier.Application.Database.ActivesInventoryInDefaultRisk.Queries.GetActivesInventoryInDefaultRisksByDefaultRiskId;

namespace Qualifier.Api.Controllers;

[Route("api/[controller]")]
[Authorize]
public class ActivesInventoryInDefaultRiskController(
    IGetActivesInventoryInDefaultRisksByDefaultRiskIdQuery getPagedQuery,
    IGetActivesInventoryInDefaultRiskByIdQuery getByIdQuery,
    ICreateActivesInventoryInDefaultRiskCommand createCommand,
    IUpdateActivesInventoryInDefaultRiskCommand updateCommand,
    IDeleteActivesInventoryInDefaultRiskCommand deleteCommand
) : ApiBaseController
{
    [HttpGet]
    public async Task<IActionResult> Get(int skip, int pageSize, int defaultRiskId, string? search)
    {
        // El BaseController ya maneja la identidad; aquí solo ejecutamos
        var res = await getPagedQuery.Execute(skip, pageSize, search ?? string.Empty, defaultRiskId);
        return ProcessResponse(res);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var res = await getByIdQuery.Execute(id);
        return ProcessResponse(res, wrapWithData: true);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateActivesInventoryInDefaultRiskDto model)
    {
        model.creationUserId = UserId;
        model.companyId = CompanyId;

        var res = await createCommand.Execute(model);
        return ProcessResponse(res, wrapWithData: true);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateActivesInventoryInDefaultRiskDto model)
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