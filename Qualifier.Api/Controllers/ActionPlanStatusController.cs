using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Qualifier.Application.Database.ActionPlanStatus.Commands.CreateActionPlanStatus;
using Qualifier.Application.Database.ActionPlanStatus.Commands.DeleteActionPlanStatus;
using Qualifier.Application.Database.ActionPlanStatus.Commands.UpdateActionPlanStatus;
using Qualifier.Application.Database.ActionPlanStatus.Queries.GetActionPlanStatusById;
using Qualifier.Application.Database.ActionPlanStatus.Queries.GetActionPlanStatussByCompanyId;
using Qualifier.Application.Database.ActionPlanStatus.Queries.GetAllActionPlanStatussByCompanyId;
using Qualifier.Common.Api;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;


namespace qualifier.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ActionPlanStatusController : ControllerBase
    {

        [HttpGet("All")]
        public async Task<IActionResult> GetAllActionPlanStatussByCompanyId([FromServices] IGetAllActionPlanStatussByCompanyIdQuery query)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            int companyId;

            bool success = int.TryParse(JwtTokenProvider.GetCompanyIdFromToken(accessToken != null ? accessToken : ""), out companyId);

            Notification notification = new Notification();
            if (!success)
                notification.addError("El usuario no está asociado a institución");

            if (notification.hasErrors())
                return BadRequest(BaseApplication.getApplicationErrorResponse(notification.errors));

            var res = await query.Execute(companyId);
            if (res.GetType() == typeof(BaseErrorResponseDto))
                return BadRequest(res);
            else
                return Ok(res);
        }


        [HttpGet()]
        public async Task<IActionResult> GetActionPlanStatussByCompanyId(int skip, int pageSize, string? search, [FromServices] IGetActionPlanStatussByCompanyIdQuery query)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            int companyId;

            bool success = int.TryParse(JwtTokenProvider.GetCompanyIdFromToken(accessToken != null ? accessToken : ""), out companyId);

            Notification notification = new Notification();
            if (!success)
                notification.addError("El usuario no está asociado a institución");

            if (notification.hasErrors())
                return BadRequest(BaseApplication.getApplicationErrorResponse(notification.errors));

            if (search == null)
                search = string.Empty;

            var res = await query.Execute(skip, pageSize, search, companyId);
            if (res.GetType() == typeof(BaseErrorResponseDto))
                return BadRequest(res);
            else
                return Ok(res);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id, [FromServices] IGetActionPlanStatusByIdQuery query)
        {
            var res = await query.Execute(id);
            if (res.GetType() == typeof(BaseErrorResponseDto))
                return BadRequest(res);
            else
                return Ok(new
                {
                    data = res
                });
        }


        [HttpPost()]
        public async Task<IActionResult> Create([FromBody] CreateActionPlanStatusDto model, [FromServices] ICreateActionPlanStatusCommand command)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            int userId;

            bool success = int.TryParse(JwtTokenProvider.GetUserIdFromToken(accessToken != null ? accessToken : ""), out userId);

            int companyId;
            bool success2 = int.TryParse(JwtTokenProvider.GetCompanyIdFromToken(accessToken != null ? accessToken : ""), out companyId);

            if (success)
                model.creationUserId = userId;

            if (success2)
                model.companyId = companyId;

            var res = await command.Execute(model);
            if (res.GetType() == typeof(BaseErrorResponseDto))
                return BadRequest(res);
            else
                return Ok(new
                {
                    data = res
                });
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] UpdateActionPlanStatusDto model, int id, [FromServices] IUpdateActionPlanStatusCommand command)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            int userId;

            bool success = int.TryParse(JwtTokenProvider.GetUserIdFromToken(accessToken != null ? accessToken : ""), out userId);

            if (success)
                model.updateUserId = userId;

            var res = await command.Execute(model, id);
            if (res.GetType() == typeof(BaseErrorResponseDto))
                return BadRequest(res);
            else
                return Ok(new
                {
                    data = res
                });
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, [FromServices] IDeleteActionPlanStatusCommand command)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            int userIdValue;

            bool success = int.TryParse(JwtTokenProvider.GetUserIdFromToken(accessToken != null ? accessToken : ""), out userIdValue);

            int userId = 0;
            if (success)
                userId = userIdValue;

            var res = await command.Execute(id, userId);
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


