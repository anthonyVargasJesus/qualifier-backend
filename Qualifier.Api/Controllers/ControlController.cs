using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Qualifier.Application.Database.Control.Commands.CreateControl;
using Qualifier.Application.Database.Control.Commands.DeleteControl;
using Qualifier.Application.Database.Control.Commands.UpdateControl;
using Qualifier.Application.Database.Control.Queries.GetAllControlsByStandardId;
using Qualifier.Application.Database.Control.Queries.GetControlById;
using Qualifier.Application.Database.Control.Queries.GetControlsByControlGroupId;
using Qualifier.Application.Database.Requirement.Queries.GetAllRequirementsByStandardId;
using Qualifier.Common.Api;
using Qualifier.Common.Application.Dto;

namespace Qualifier.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControlController : ControllerBase
    {

        [HttpGet("All")]
        public async Task<IActionResult> GetAll(int standardId, [FromServices] IGetAllControlsByStandardIdQuery query)
        {

            var res = await query.Execute(standardId);
            if (res.GetType() == typeof(BaseErrorResponseDto))
                return BadRequest(res);
            else
                return Ok(res);
        }

        [HttpGet()]
        public async Task<IActionResult> Get(int skip, int pageSize, string? search, int controlGroupId, [FromServices] IGetControlsByControlGroupIdQuery query)
        {
            if (search == null)
                search = string.Empty;

            var res = await query.Execute(skip, pageSize, search, controlGroupId);
            if (res.GetType() == typeof(BaseErrorResponseDto))
                return BadRequest(res);
            else
                return Ok(res);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id, [FromServices] IGetControlByIdQuery getControlByIdQuery)
        {
            var res = await getControlByIdQuery.Execute(id);
            if (res.GetType() == typeof(BaseErrorResponseDto))

                return BadRequest(res);
            else
                return Ok(new
                {
                    data = res
                });
        }

        [HttpPost()]
        public async Task<IActionResult> Create([FromBody] CreateControlDto model, [FromServices] ICreateControlCommand createControlCommand)
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

            var res = await createControlCommand.Execute(model);
            if (res.GetType() == typeof(BaseErrorResponseDto))
                return BadRequest(res);
            else

                return Ok(new
                {
                    data = res
                });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] UpdateControlDto model, int id, [FromServices] IUpdateControlCommand updateControlCommand)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            int userId;

            bool success = int.TryParse(JwtTokenProvider.GetUserIdFromToken(accessToken), out userId);

            if (success)
                model.updateUserId = userId;

            var res = await updateControlCommand.Execute(model, id);
            if (res.GetType() == typeof(BaseErrorResponseDto))
                return BadRequest(res);
            else
                return Ok(new
                {
                    data = res
                });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> delete(int id, [FromServices] IDeleteControlCommand deleteControlCommand)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            int userIdValue;

            bool success = int.TryParse(JwtTokenProvider.GetUserIdFromToken(accessToken), out userIdValue);

            int userId = 0;
            if (success)
                userId = userIdValue;

            var res = await deleteControlCommand.Execute(id, userId);
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
