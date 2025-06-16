using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Qualifier.Api.Helpers;
using Qualifier.Application.Database.Indicator.Commands.CreateIndicator;
using Qualifier.Application.Database.Indicator.Commands.DeleteIndicator;
using Qualifier.Application.Database.Indicator.Commands.UpdateIndicator;
using Qualifier.Application.Database.Indicator.Queries.GetIndicatorById;
using Qualifier.Application.Database.Indicator.Queries.GetIndicatorsByCompanyId;
using Qualifier.Common.Api;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;

namespace Qualifier.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class IndicatorController : ControllerBase
    {
        [HttpGet()]
        public async Task<IActionResult> Get(int skip, int pageSize, string? search, [FromServices] IGetIndicatorsByCompanyIdQuery query)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            int companyId = HttpContext.GetCompanyIdAsync(accessToken);

            Notification notification = new Notification();
            if (companyId == CompanyConstants.NO_COMPANY_ASSOCIATED)
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
        public async Task<IActionResult> Get(int id, [FromServices] IGetIndicatorByIdQuery getIndicatorByIdQuery)
        {
            var res = await getIndicatorByIdQuery.Execute(id);
            if (res.GetType() == typeof(BaseErrorResponseDto))

                return BadRequest(res);
            else
                return Ok(new
                {
                    data = res
                });
        }

        [HttpPost()]
        public async Task<IActionResult> Create([FromBody] CreateIndicatorDto model, 
            [FromServices] ICreateIndicatorCommand createIndicatorCommand)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            int companyId = HttpContext.GetCompanyIdAsync(accessToken);

            Notification notification = new Notification();
            if (companyId == CompanyConstants.NO_COMPANY_ASSOCIATED)
                notification.addError("El usuario no está asociado a institución");
            else
                model.companyId = companyId;

            model.creationUserId = HttpContext.GetUserIdAsync(accessToken);

            var res = await createIndicatorCommand.Execute(model);
            if (res.GetType() == typeof(BaseErrorResponseDto))
                return BadRequest(res);
            else
                return Ok(new
                {
                    data = res
                });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] UpdateIndicatorDto model, int id, 
            [FromServices] IUpdateIndicatorCommand updateIndicatorCommand)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            model.updateUserId = HttpContext.GetUserIdAsync(accessToken);

            var res = await updateIndicatorCommand.Execute(model, id);
            if (res.GetType() == typeof(BaseErrorResponseDto))
                return BadRequest(res);
            else
                return Ok(new
                {
                    data = res
                });
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> delete(int id, 
            [FromServices] IDeleteIndicatorCommand deleteIndicatorCommand)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            int userId = HttpContext.GetUserIdAsync(accessToken);

            var res = await deleteIndicatorCommand.Execute(id, userId);
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
