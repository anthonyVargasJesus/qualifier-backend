using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Qualifier.Application.Database.UserControlGroup.Commands.SetUserControlGroups;
using Qualifier.Application.Database.UserControlGroup.Queries.GetUserControlGroupsByUserId;
using Qualifier.Common.Api;
using Qualifier.Common.Application.Dto;

namespace Qualifier.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserControlGroupController : ControllerBase
    {
        [HttpGet()]
        public async Task<IActionResult> Get(int userId, int standardId, [FromServices] IGetUserControlGroupsByUserIdQuery query)
        {
            var res = await query.Execute(userId, standardId);
            if (res.GetType() == typeof(BaseErrorResponseDto))
                return BadRequest(res);
            else
                return Ok(res);
        }

        [HttpPut()]
        public async Task<IActionResult> Put([FromBody] SetUserControlGroupsDto model, [FromServices] ISetUserControlGroupsCommand command)
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
