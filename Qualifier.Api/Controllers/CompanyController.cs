using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Qualifier.Application.Database.ActiveType.Commands.UpdateActiveType;
using Qualifier.Application.Database.Company.Commands.UpdateCompany;
using Qualifier.Application.Database.Company.Queries.GetCompanyById;
using Qualifier.Common.Api;
using Qualifier.Common.Application.Dto;

namespace Qualifier.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id, [FromServices] IGetCompanyByIdQuery getCompanyByIdQuery)
        {
            var res = await getCompanyByIdQuery.Execute(id);
            if (res.GetType() == typeof(BaseErrorResponseDto))

                return BadRequest(res);
            else
                return Ok(new
                {
                    data = res
                });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] UpdateCompanyDto model, int id,
    [FromServices] IUpdateCompanyCommand updateCompanyCommand)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            int userId;

            bool success = int.TryParse(JwtTokenProvider.GetUserIdFromToken(accessToken), out userId);

            if (success)
                model.updateUserId = userId;

            var res = await updateCompanyCommand.Execute(model, id);
            if (res.GetType() == typeof(BaseErrorResponseDto))
                return BadRequest(res);
            else
                return Ok(new
                {
                    data = res
                });
        }


    }
}
