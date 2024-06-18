using Microsoft.AspNetCore.Mvc;
using Qualifier.Application.Database.RoleInUser.Commands.CreateRoleInUser;
using Qualifier.Application.Database.RoleInUser.Commands.DeleteRoleInUser;
using Qualifier.Application.Database.RoleInUser.Commands.UpdateRoleInUser;
using Qualifier.Application.Database.RoleInUser.Queries.GetRoleInUserById;
using Qualifier.Common.Application.Dto;
using Microsoft.AspNetCore.Authorization;
using Qualifier.Application.Database.RoleInUser.Queries.GetRoleInUsersByUserId;
using Microsoft.AspNetCore.Authentication;
using Qualifier.Common.Api;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Application.Database.RoleInUser.Queries.GetAllRoleInUsersByUserId;



namespace Qualifier.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RoleInUserController : ControllerBase
    {
        [HttpGet()]
        public async Task<IActionResult> Get(int skip, int pageSize, string? search, int userId, [FromServices] IGetRoleInUsersByUserIdQuery query)
        {

            if (search == null)
                search = string.Empty;

            var res = await query.Execute(skip, pageSize, search, userId);
            if (res.GetType() == typeof(BaseErrorResponseDto))
                return BadRequest(res);
            else
                return Ok(res);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id, [FromServices] IGetRoleInUserByIdQuery getRoleInUserByIdQuery)
        {
            var res = await getRoleInUserByIdQuery.Execute(id);
            if (res.GetType() == typeof(BaseErrorResponseDto))

                return BadRequest(res);
            else
                return Ok(new
                {
                    data = res
                });
        }

        [HttpPost()]
        public async Task<IActionResult> Create([FromBody] CreateRoleInUserDto model,
            [FromServices] ICreateRoleInUserCommand createRoleInUserCommand)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            int userId;

            bool success = int.TryParse(JwtTokenProvider.GetUserIdFromToken(accessToken), out userId);

            int companyId;
            bool success2 = int.TryParse(JwtTokenProvider.GetCompanyIdFromToken(accessToken), out companyId);

            if (success)
                model.creationUserId = userId;

            if (success2)
                model.companyId = companyId;

            var res = await createRoleInUserCommand.Execute(model);
            if (res.GetType() == typeof(BaseErrorResponseDto))
                return BadRequest(res);
            else
                return Ok(new
                {
                    data = res
                });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] UpdateRoleInUserDto model, int id,
            [FromServices] IUpdateRoleInUserCommand updateRoleInUserCommand)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            int userId;

            bool success = int.TryParse(JwtTokenProvider.GetUserIdFromToken(accessToken), out userId);

            if (success)
                model.updateUserId = userId;

            var res = await updateRoleInUserCommand.Execute(model, id);
            if (res.GetType() == typeof(BaseErrorResponseDto))
                return BadRequest(res);
            else
                return Ok(new
                {
                    data = res
                });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> delete(int id, [FromServices] IDeleteRoleInUserCommand deleteRoleInUserCommand)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            int userIdValue;

            bool success = int.TryParse(JwtTokenProvider.GetUserIdFromToken(accessToken), out userIdValue);

            int userId = 0;
            if (success)
                userId = userIdValue;

            var res = await deleteRoleInUserCommand.Execute(id, userId);
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
