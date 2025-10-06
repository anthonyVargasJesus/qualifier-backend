using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Qualifier.Application.Database.ActionPlan.Commands.CreateActionPlan;
using Qualifier.Application.Database.ActionPlan.Commands.DeleteActionPlan;
using Qualifier.Application.Database.ActionPlan.Commands.UpdateActionPlan;
using Qualifier.Application.Database.ActionPlan.Queries.GetActionPlanById;
using Qualifier.Application.Database.ActionPlan.Queries.GetActionPlansByBreachId;
using Qualifier.Common.Api;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;


namespace qualifier.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ActionPlanController : ControllerBase
    {

        [HttpGet()]
        public async Task<IActionResult> GetActionPlansByBreachId(int skip, int pageSize, 
            string? search, int breachId, [FromServices] IGetActionPlansByBreachIdQuery query)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            int companyId;

            bool success = int.TryParse(JwtTokenProvider.GetCompanyIdFromToken(
                accessToken != null ? accessToken : ""), out companyId);

            Notification notification = new Notification();
            if (!success)
                notification.addError("El usuario no está asociado a institución");

            if (notification.hasErrors())
                return BadRequest(BaseApplication.getApplicationErrorResponse(notification.errors));

            if (search == null)
                search = string.Empty;

            var res = await query.Execute(skip, pageSize, search, breachId);
            if (res.GetType() == typeof(BaseErrorResponseDto))
                return BadRequest(res);
            else
                return Ok(res);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id, [FromServices] IGetActionPlanByIdQuery query)
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
        public async Task<IActionResult> Create([FromBody] CreateActionPlanDto model, [FromServices] ICreateActionPlanCommand command)
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
        public async Task<IActionResult> Update([FromBody] UpdateActionPlanDto model, int id, [FromServices] IUpdateActionPlanCommand command)
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
        public async Task<IActionResult> Delete(int id, [FromServices] IDeleteActionPlanCommand command)
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


