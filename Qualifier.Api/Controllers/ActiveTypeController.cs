using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Qualifier.Application.Database.ActiveType.Commands.CreateActiveType;
using Qualifier.Application.Database.ActiveType.Commands.DeleteActiveType;
using Qualifier.Application.Database.ActiveType.Commands.UpdateActiveType;
using Qualifier.Application.Database.ActiveType.Queries.GetActiveTypeById;
using Qualifier.Application.Database.ActiveType.Queries.GetActiveTypesByCompanyId;
using Qualifier.Application.Database.ActiveType.Queries.GetAllActiveTypesByCompanyId;

namespace Qualifier.Api.Controllers;

[Route("api/[controller]")]
[Authorize]
public class ActiveTypeController(
    IGetAllActiveTypesByCompanyIdQuery getAllQuery,
    IGetActiveTypesByCompanyIdQuery getPagedQuery,
    IGetActiveTypeByIdQuery getByIdQuery,
    ICreateActiveTypeCommand createCommand,
    IUpdateActiveTypeCommand updateCommand,
    IDeleteActiveTypeCommand deleteCommand
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
    public async Task<IActionResult> Create([FromBody] CreateActiveTypeDto model)
    {
        model.creationUserId = UserId;
        model.companyId = CompanyId;

        var res = await createCommand.Execute(model);
        return ProcessResponse(res, wrapWithData: true);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateActiveTypeDto model)
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