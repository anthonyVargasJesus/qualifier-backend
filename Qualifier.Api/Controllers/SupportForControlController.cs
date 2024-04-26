using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Qualifier.Application.Database.SupportForControl.Commands.CreateSupportForControl;
using Qualifier.Application.Database.SupportForControl.Commands.DeleteSupportForControl;
using Qualifier.Application.Database.SupportForControl.Commands.UpdateSupportForControl;
using Qualifier.Application.Database.SupportForControl.Queries.GetSupportForControlById;
using Qualifier.Application.Database.SupportForControl.Queries.GetSupportForControlsByDocumentationId;
using Qualifier.Common.Api;
using Qualifier.Common.Application.Dto;

namespace Qualifier.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupportForControlController : ControllerBase
    {
            [HttpGet()]
            public async Task<IActionResult> Get(int skip, int pageSize, string? search, int documentationId, [FromServices] IGetSupportForControlsByDocumentationIdQuery query)
            {
                if (search == null)
                    search = string.Empty;

                var res = await query.Execute(skip, pageSize, search, documentationId);
                if (res.GetType() == typeof(BaseErrorResponseDto))
                    return BadRequest(res);
                else
                    return Ok(res);
            }


            [HttpGet("{id}")]
            public async Task<IActionResult> Get(int id, [FromServices] IGetSupportForControlByIdQuery getSupportForControlByIdQuery)
            {
                var res = await getSupportForControlByIdQuery.Execute(id);
                if (res.GetType() == typeof(BaseErrorResponseDto))

                    return BadRequest(res);
                else
                    return Ok(new
                    {
                        data = res
                    });
            }

            [HttpPost()]
            public async Task<IActionResult> Create([FromBody] CreateSupportForControlDto model, [FromServices] ICreateSupportForControlCommand createSupportForControlCommand)
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

                var res = await createSupportForControlCommand.Execute(model);
                if (res.GetType() == typeof(BaseErrorResponseDto))
                    return BadRequest(res);
                else
                    return Ok(new
                    {
                        data = res
                    });
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> Put([FromBody] UpdateSupportForControlDto model, int id, [FromServices] IUpdateSupportForControlCommand updateSupportForControlCommand)
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                int userId;

                bool success = int.TryParse(JwtTokenProvider.GetUserIdFromToken(accessToken), out userId);

                if (success)
                    model.updateUserId = userId;

                var res = await updateSupportForControlCommand.Execute(model, id);
                if (res.GetType() == typeof(BaseErrorResponseDto))
                    return BadRequest(res);
                else
                    return Ok(new
                    {
                        data = res
                    });
            }


            [HttpDelete("{id}")]
            public async Task<IActionResult> delete(int id, [FromServices] IDeleteSupportForControlCommand deleteSupportForControlCommand)
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                int userIdValue;

                bool success = int.TryParse(JwtTokenProvider.GetUserIdFromToken(accessToken), out userIdValue);

                int userId = 0;
                if (success)
                    userId = userIdValue;

                var res = await deleteSupportForControlCommand.Execute(id, userId);
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
