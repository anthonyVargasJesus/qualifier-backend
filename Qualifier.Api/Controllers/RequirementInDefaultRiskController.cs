using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Qualifier.Application.Database.RequirementInDefaultRisk.Commands.CreateRequirementInDefaultRisk;
using Qualifier.Application.Database.RequirementInDefaultRisk.Commands.DeleteRequirementInDefaultRisk;
using Qualifier.Application.Database.RequirementInDefaultRisk.Commands.UpdateRequirementInDefaultRisk;
using Qualifier.Application.Database.RequirementInDefaultRisk.Queries.GetRequirementInDefaultRiskById;
using Qualifier.Application.Database.RequirementInDefaultRisk.Queries.GetRequirementInDefaultRisksByRequirementId;
using Qualifier.Common.Api;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;

namespace Qualifier.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RequirementInDefaultRiskController : ControllerBase
    {
        [HttpGet()]
        public async Task<IActionResult> Get(int skip, int pageSize, int requirementId, string? search, [FromServices] IGetRequirementInDefaultRisksByRequirementIdQuery query)
        {
            Notification notification = new Notification();

            if (notification.hasErrors())
                return BadRequest(BaseApplication.getApplicationErrorResponse(notification.errors));

            if (search == null)
                search = string.Empty;

            var res = await query.Execute(skip, pageSize, search, requirementId);
            if (res.GetType() == typeof(BaseErrorResponseDto))
                return BadRequest(res);
            else
                return Ok(res);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id, [FromServices] IGetRequirementInDefaultRiskByIdQuery getRequirementInDefaultRiskByIdQuery)
        {
            var res = await getRequirementInDefaultRiskByIdQuery.Execute(id);
            if (res.GetType() == typeof(BaseErrorResponseDto))

                return BadRequest(res);
            else
                return Ok(new
                {
                    data = res
                });
        }

        [HttpPost()]
        public async Task<IActionResult> Create([FromBody] CreateRequirementInDefaultRiskDto model, [FromServices] ICreateRequirementInDefaultRiskCommand createRequirementInDefaultRiskCommand)
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

            var res = await createRequirementInDefaultRiskCommand.Execute(model);
            if (res.GetType() == typeof(BaseErrorResponseDto))
                return BadRequest(res);
            else

                return Ok(new
                {
                    data = res
                });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] UpdateRequirementInDefaultRiskDto model, int id, [FromServices] IUpdateRequirementInDefaultRiskCommand updateRequirementInDefaultRiskCommand)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            int userId;

            bool success = int.TryParse(JwtTokenProvider.GetUserIdFromToken(accessToken), out userId);

            if (success)
                model.updateUserId = userId;

            var res = await updateRequirementInDefaultRiskCommand.Execute(model, id);
            if (res.GetType() == typeof(BaseErrorResponseDto))
                return BadRequest(res);
            else
                return Ok(new
                {
                    data = res
                });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> delete(int id, [FromServices] IDeleteRequirementInDefaultRiskCommand deleteRequirementInDefaultRiskCommand)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            int userIdValue;

            bool success = int.TryParse(JwtTokenProvider.GetUserIdFromToken(accessToken), out userIdValue);

            int userId = 0;
            if (success)
                userId = userIdValue;

            var res = await deleteRequirementInDefaultRiskCommand.Execute(id, userId);
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
