using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Qualifier.Application.Database.BreachStatus.Queries.GetAllBreachStatussByCompanyId;

namespace Qualifier.Api.Controllers;

[Route("api/[controller]")]
[Authorize]
public class BreachStatusController(
    IGetAllBreachStatussByCompanyIdQuery getAllQuery
) : ApiBaseController
{
    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        if (CompanyId == 0)
            return CompanyRequiredError();

        var res = await getAllQuery.Execute(CompanyId);
        return ProcessResponse(res);
    }
}