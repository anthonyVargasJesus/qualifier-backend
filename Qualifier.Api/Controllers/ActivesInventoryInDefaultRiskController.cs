using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Qualifier.Application.Database.ActivesInventoryInDefaultRisk.Commands.CreateActivesInventoryInDefaultRisk;
using Qualifier.Application.Database.ActivesInventoryInDefaultRisk.Commands.DeleteActivesInventoryInDefaultRisk;
using Qualifier.Application.Database.ActivesInventoryInDefaultRisk.Commands.UpdateActivesInventoryInDefaultRisk;
using Qualifier.Application.Database.ActivesInventoryInDefaultRisk.Queries.GetActivesInventoryInDefaultRiskById;
using Qualifier.Application.Database.ActivesInventoryInDefaultRisk.Queries.GetActivesInventoryInDefaultRisksByDefaultRiskId;
using Qualifier.Common.Api;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.NotificationPattern;

namespace Qualifier.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivesInventoryInDefaultRiskController : ControllerBase
    {
        [HttpGet()]
        public async Task<IActionResult> Get(int skip, int pageSize, int defaultRiskId, string? search, [FromServices] IGetActivesInventoryInDefaultRisksByDefaultRiskIdQuery query)
        {
            Notification notification = new Notification();

            if (notification.hasErrors())
                return BadRequest(Common.Application.Service.BaseApplication.getApplicationErrorResponse(notification.errors));

            if (search == null)
                search = string.Empty;

            var res = await query.Execute(skip, pageSize, search, defaultRiskId);
            if (res.GetType() == typeof(BaseErrorResponseDto))
                return BadRequest(res);
            else
                return Ok(res);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id, [FromServices] IGetActivesInventoryInDefaultRiskByIdQuery query)
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
        public async Task<IActionResult> Create([FromBody] CreateActivesInventoryInDefaultRiskDto model, [FromServices] ICreateActivesInventoryInDefaultRiskCommand createControlInDefaultRiskCommand)
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

            var res = await createControlInDefaultRiskCommand.Execute(model);
            if (res.GetType() == typeof(BaseErrorResponseDto))
                return BadRequest(res);
            else

                return Ok(new
                {
                    data = res
                });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] UpdateActivesInventoryInDefaultRiskDto model, int id, [FromServices] IUpdateActivesInventoryInDefaultRiskCommand updateControlInDefaultRiskCommand)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            int userId;

            bool success = int.TryParse(JwtTokenProvider.GetUserIdFromToken(accessToken), out userId);

            if (success)
                model.updateUserId = userId;

            var res = await updateControlInDefaultRiskCommand.Execute(model, id);
            if (res.GetType() == typeof(BaseErrorResponseDto))
                return BadRequest(res);
            else
                return Ok(new
                {
                    data = res
                });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> delete(int id, [FromServices] IDeleteActivesInventoryInDefaultRiskCommand deleteControlInDefaultRiskCommand)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            int userIdValue;

            bool success = int.TryParse(JwtTokenProvider.GetUserIdFromToken(accessToken), out userIdValue);

            int userId = 0;
            if (success)
                userId = userIdValue;

            var res = await deleteControlInDefaultRiskCommand.Execute(id, userId);
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
