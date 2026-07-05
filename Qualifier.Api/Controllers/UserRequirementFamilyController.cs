using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Qualifier.Application.Database.UserRequirementFamily.Commands.SetUserRequirementFamilies;
using Qualifier.Application.Database.UserRequirementFamily.Queries.GetUserRequirementFamiliesByUserId;
using Qualifier.Common.Api;
using Qualifier.Common.Application.Dto;

namespace Qualifier.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserRequirementFamilyController : ControllerBase
    {
        [HttpGet()]
        public async Task<IActionResult> Get(int userId, int standardId, [FromServices] IGetUserRequirementFamiliesByUserIdQuery query)
        {
            var res = await query.Execute(userId, standardId);
            if (res.GetType() == typeof(BaseErrorResponseDto))
                return BadRequest(res);
            else
                return Ok(res);
        }

        [HttpPut()]
        public async Task<IActionResult> Put([FromBody] SetUserRequirementFamiliesDto model, [FromServices] ISetUserRequirementFamiliesCommand command)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            int userId;
            if (int.TryParse(JwtTokenProvider.GetUserIdFromToken(accessToken), out userId))
                model.creationUserId = userId;

            int companyId;
            if (int.TryParse(JwtTokenProvider.GetCompanyIdFromToken(accessToken), out companyId))
                model.companyId = companyId;

            var res = await command.Execute(model);
            if (res.GetType() == typeof(BaseErrorResponseDto))
                return BadRequest(res);
            else
                return Ok(new { data = res });
        }
    }
}
