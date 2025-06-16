using Microsoft.AspNetCore.Mvc;
using Qualifier.Application.Database.ActivesInventory.Commands.CreateActivesInventory;
using Qualifier.Application.Database.ActivesInventory.Commands.DeleteActivesInventory;
using Qualifier.Application.Database.ActivesInventory.Commands.UpdateActivesInventory;
using Qualifier.Application.Database.ActivesInventory.Queries.GetActivesInventoryById;
using Qualifier.Common.Application.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Qualifier.Common.Api;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;
using Qualifier.Application.Database.ActivesInventory.Queries.GetActivesInventoriesByCompanyId;
using Qualifier.Application.Database.ActiveType.Queries.GetAllActiveTypesByCompanyId;
using Qualifier.Application.Database.ActivesInventory.Queries.GetAllActivesInventoriesByCompanyId;
using Qualifier.Api.Helpers;



namespace Qualifier.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ActivesInventoryController : ControllerBase
    {
        [HttpGet("All")]
        public async Task<IActionResult> GetAll([FromServices] IGetAllActivesInventoriesByCompanyIdQuery query)
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
        public async Task<IActionResult> Get(int skip, int pageSize, string? search, 
            [FromServices] IGetActivesInventoriesByCompanyIdQuery query)
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
        public async Task<IActionResult> Get(int id, [FromServices] IGetActivesInventoryByIdQuery getActivesInventoryByIdQuery)
        {
            var res = await getActivesInventoryByIdQuery.Execute(id);
            if (res.GetType() == typeof(BaseErrorResponseDto))

                return BadRequest(res);
            else
                return Ok(new
                {
                    data = res
                });
        }

        [HttpPost()]
        public async Task<IActionResult> Create([FromBody] CreateActivesInventoryDto model, 
            [FromServices] ICreateActivesInventoryCommand createActivesInventoryCommand)
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

            var res = await createActivesInventoryCommand.Execute(model);
            if (res.GetType() == typeof(BaseErrorResponseDto))
                return BadRequest(res);
            else
                return Ok(new
                {
                    data = res
                });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] UpdateActivesInventoryDto model, int id, 
            [FromServices] IUpdateActivesInventoryCommand updateActivesInventoryCommand)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            int userId;

            bool success = int.TryParse(JwtTokenProvider.GetUserIdFromToken(accessToken), out userId);

            if (success)
                model.updateUserId = userId;

            var res = await updateActivesInventoryCommand.Execute(model, id);
            if (res.GetType() == typeof(BaseErrorResponseDto))
                return BadRequest(res);
            else
                return Ok(new
                {
                    data = res
                });
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> delete(int id, [FromServices] IDeleteActivesInventoryCommand deleteActivesInventoryCommand)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            int userIdValue;

            bool success = int.TryParse(JwtTokenProvider.GetUserIdFromToken(accessToken), out userIdValue);

            int userId = 0;
            if (success)
                userId = userIdValue;

            var res = await deleteActivesInventoryCommand.Execute(id, userId);
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
