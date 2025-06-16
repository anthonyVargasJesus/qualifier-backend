using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Qualifier.Api.Helpers;
using Qualifier.Application.Database.Location.Commands.CreateLocation;
using Qualifier.Application.Database.Location.Commands.DeleteLocation;
using Qualifier.Application.Database.Location.Commands.UpdateLocation;
using Qualifier.Application.Database.Location.Queries.GetAllLocationsByCompanyId;
using Qualifier.Application.Database.Location.Queries.GetLocationById;
using Qualifier.Application.Database.Location.Queries.GetLocationsByCompanyId;
using Qualifier.Common.Api;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;



namespace Qualifier.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LocationController : ControllerBase
    {
        [HttpGet("All")]
        public async Task<IActionResult> GetAll([FromServices] IGetAllLocationsByCompanyIdQuery query)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            int companyId = HttpContext.GetCompanyIdAsync(accessToken);

            Notification notification = new Notification();
            if (companyId == CompanyConstants.NO_COMPANY_ASSOCIATED)
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
        public async Task<IActionResult> Get(int skip, int pageSize, string? search, [FromServices] IGetLocationsByCompanyIdQuery query)
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
        public async Task<IActionResult> Get(int id, [FromServices] IGetLocationByIdQuery getLocationByIdQuery)
        {
            var res = await getLocationByIdQuery.Execute(id);
            if (res.GetType() == typeof(BaseErrorResponseDto))

                return BadRequest(res);
            else
                return Ok(new
                {
                    data = res
                });
        }

        [HttpPost()]
        public async Task<IActionResult> Create([FromBody] CreateLocationDto model,
            [FromServices] ICreateLocationCommand createLocationCommand)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            int companyId = HttpContext.GetCompanyIdAsync(accessToken);

            Notification notification = new Notification();
            if (companyId == CompanyConstants.NO_COMPANY_ASSOCIATED)
                notification.addError("El usuario no está asociado a institución");
            else
                model.companyId = companyId;

            model.creationUserId = HttpContext.GetUserIdAsync(accessToken);

            var res = await createLocationCommand.Execute(model);
            if (res.GetType() == typeof(BaseErrorResponseDto))
                return BadRequest(res);
            else
                return Ok(new
                {
                    data = res
                });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] UpdateLocationDto model, int id, [FromServices] IUpdateLocationCommand updateLocationCommand)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            model.updateUserId = HttpContext.GetUserIdAsync(accessToken);

            var res = await updateLocationCommand.Execute(model, id);
            if (res.GetType() == typeof(BaseErrorResponseDto))
                return BadRequest(res);
            else
                return Ok(new
                {
                    data = res
                });
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> delete(int id, [FromServices] IDeleteLocationCommand deleteLocationCommand)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            int userId = HttpContext.GetUserIdAsync(accessToken);

            var res = await deleteLocationCommand.Execute(id, userId);
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
