using Microsoft.AspNetCore.Mvc;
using Qualifier.Application.Database.ResidualRisk.Commands.CreateResidualRisk;
using Qualifier.Application.Database.ResidualRisk.Commands.DeleteResidualRisk;
using Qualifier.Application.Database.ResidualRisk.Commands.UpdateResidualRisk;
using Qualifier.Application.Database.ResidualRisk.Queries.GetResidualRiskById;
using Qualifier.Common.Application.Dto;
using Microsoft.AspNetCore.Authorization;
using Qualifier.Application.Database.ResidualRisk.Queries.GetResidualRisksByCompanyId;
using Microsoft.AspNetCore.Authentication;
using Qualifier.Common.Api;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Application.Database.ResidualRisk.Queries.GetAllResidualRisksByCompanyId;



namespace Qualifier.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ResidualRiskController : ControllerBase
    {
        [HttpGet("All")]
        public async Task<IActionResult> GetAll([FromServices] IGetAllResidualRisksByCompanyIdQuery query)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            int companyId;

            bool success = int.TryParse(JwtTokenProvider.GetCompanyIdFromToken(accessToken), out companyId);

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
        public async Task<IActionResult> Get(int skip, int pageSize, string? search, [FromServices] IGetResidualRisksByCompanyIdQuery query)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            int companyId;

            bool success = int.TryParse(JwtTokenProvider.GetCompanyIdFromToken(accessToken), out companyId);

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
        public async Task<IActionResult> Get(int id, [FromServices] IGetResidualRiskByIdQuery getResidualRiskByIdQuery)
        {
            var res = await getResidualRiskByIdQuery.Execute(id);
            if (res.GetType() == typeof(BaseErrorResponseDto))

                return BadRequest(res);
            else
                return Ok(new
                {
                    data = res
                });
        }

        [HttpPost()]
        public async Task<IActionResult> Create([FromBody] CreateResidualRiskDto model,
            [FromServices] ICreateResidualRiskCommand createResidualRiskCommand)
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

            var res = await createResidualRiskCommand.Execute(model);
            if (res.GetType() == typeof(BaseErrorResponseDto))
                return BadRequest(res);
            else
                return Ok(new
                {
                    data = res
                });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] UpdateResidualRiskDto model, int id,
            [FromServices] IUpdateResidualRiskCommand updateResidualRiskCommand)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            int userId;

            bool success = int.TryParse(JwtTokenProvider.GetUserIdFromToken(accessToken), out userId);

            if (success)
                model.updateUserId = userId;

            var res = await updateResidualRiskCommand.Execute(model, id);
            if (res.GetType() == typeof(BaseErrorResponseDto))
                return BadRequest(res);
            else
                return Ok(new
                {
                    data = res
                });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> delete(int id, [FromServices] IDeleteResidualRiskCommand deleteResidualRiskCommand)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            int userIdValue;

            bool success = int.TryParse(JwtTokenProvider.GetUserIdFromToken(accessToken), out userIdValue);

            int userId = 0;
            if (success)
                userId = userIdValue;

            var res = await deleteResidualRiskCommand.Execute(id, userId);
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
