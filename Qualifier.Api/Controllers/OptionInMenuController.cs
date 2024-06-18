using Microsoft.AspNetCore.Mvc;
using Qualifier.Application.Database.OptionInMenu.Commands.CreateOptionInMenu;
using Qualifier.Application.Database.OptionInMenu.Commands.DeleteOptionInMenu;
using Qualifier.Application.Database.OptionInMenu.Commands.UpdateOptionInMenu;
using Qualifier.Application.Database.OptionInMenu.Queries.GetOptionInMenuById;
using Qualifier.Common.Application.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Qualifier.Common.Api;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Application.Database.OptionInMenu.Queries.GetOptionInMenusByMenuId;



namespace Qualifier.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OptionInMenuController : ControllerBase
    {
        [HttpGet()]
        public async Task<IActionResult> Get(int skip, int pageSize, string? search, int menuId, [FromServices] IGetOptionInMenusByMenuIdQuery query)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            Notification notification = new Notification();

            if (notification.hasErrors())
                return BadRequest(BaseApplication.getApplicationErrorResponse(notification.errors));

            if (search == null)
                search = string.Empty;

            var res = await query.Execute(skip, pageSize, search, menuId);
            if (res.GetType() == typeof(BaseErrorResponseDto))
                return BadRequest(res);
            else
                return Ok(res);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id, [FromServices] IGetOptionInMenuByIdQuery getOptionInMenuByIdQuery)
        {
            var res = await getOptionInMenuByIdQuery.Execute(id);
            if (res.GetType() == typeof(BaseErrorResponseDto))

                return BadRequest(res);
            else
                return Ok(new
                {
                    data = res
                });
        }

        [HttpPost()]
        public async Task<IActionResult> Create([FromBody] CreateOptionInMenuDto model,
            [FromServices] ICreateOptionInMenuCommand createOptionInMenuCommand)
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

            var res = await createOptionInMenuCommand.Execute(model);
            if (res.GetType() == typeof(BaseErrorResponseDto))
                return BadRequest(res);
            else
                return Ok(new
                {
                    data = res
                });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] UpdateOptionInMenuDto model, int id,
            [FromServices] IUpdateOptionInMenuCommand updateOptionInMenuCommand)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            int userId;

            bool success = int.TryParse(JwtTokenProvider.GetUserIdFromToken(accessToken), out userId);

            if (success)
                model.updateUserId = userId;

            var res = await updateOptionInMenuCommand.Execute(model, id);
            if (res.GetType() == typeof(BaseErrorResponseDto))
                return BadRequest(res);
            else
                return Ok(new
                {
                    data = res
                });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> delete(int id, [FromServices] IDeleteOptionInMenuCommand deleteOptionInMenuCommand)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            int userIdValue;

            bool success = int.TryParse(JwtTokenProvider.GetUserIdFromToken(accessToken), out userIdValue);

            int userId = 0;
            if (success)
                userId = userIdValue;

            var res = await deleteOptionInMenuCommand.Execute(id, userId);
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
