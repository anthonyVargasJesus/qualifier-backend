using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Qualifier.Application.Database.Company.Commands.UpdateCompany;
using Qualifier.Application.Database.Company.Queries.GetCompanyById;

namespace Qualifier.Api.Controllers;

[Route("api/[controller]")]
[Authorize]
public class CompanyController(
    IGetCompanyByIdQuery getByIdQuery,
    IUpdateCompanyCommand updateCommand
) : ApiBaseController
{
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var res = await getByIdQuery.Execute(id);
        return ProcessResponse(res, wrapWithData: true);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateCompanyDto model)
    {
        // Usamos la propiedad UserId heredada del ApiBaseController
        model.updateUserId = UserId;

        var res = await updateCommand.Execute(model, id);
        return ProcessResponse(res, wrapWithData: true);
    }
}