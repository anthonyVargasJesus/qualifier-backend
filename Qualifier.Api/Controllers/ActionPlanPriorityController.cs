using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Qualifier.Application.Database.ActionPlanPriority.Commands.CreateActionPlanPriority;
using Qualifier.Application.Database.ActionPlanPriority.Commands.DeleteActionPlanPriority;
using Qualifier.Application.Database.ActionPlanPriority.Commands.UpdateActionPlanPriority;
using Qualifier.Application.Database.ActionPlanPriority.Queries.GetActionPlanPrioritiesByCompanyId;
using Qualifier.Application.Database.ActionPlanPriority.Queries.GetActionPlanPriorityById;
using Qualifier.Application.Database.ActionPlanPriority.Queries.GetAllActionPlanPrioritiesByCompanyId;
using Qualifier.Common.Api;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;


namespace qualifier.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ActionPlanPriorityController : ControllerBase
    {

        [HttpGet("All")]
        public async Task<IActionResult> GetAllActionPlanPrioritiesByCompanyId([FromServices] IGetAllActionPlanPrioritiesByCompanyIdQuery query)
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
        public async Task<IActionResult> GetActionPlanPrioritiesByCompanyId(int skip, int pageSize, string? search, [FromServices] IGetActionPlanPrioritiesByCompanyIdQuery query)
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
        public async Task<IActionResult> Get(int id, [FromServices] IGetActionPlanPriorityByIdQuery query)
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
        public async Task<IActionResult> Create([FromBody] CreateActionPlanPriorityDto model, [FromServices] ICreateActionPlanPriorityCommand command)
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
        public async Task<IActionResult> Update([FromBody] UpdateActionPlanPriorityDto model, int id, [FromServices] IUpdateActionPlanPriorityCommand command)
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
        public async Task<IActionResult> Delete(int id, [FromServices] IDeleteActionPlanPriorityCommand command)
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


