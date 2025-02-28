using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Qualifier.Application.Database.Scope.Commands.CreateScope;
using Qualifier.Application.Database.Scope.Commands.DeleteScope;
using Qualifier.Application.Database.Scope.Commands.UpdateScope;
using Qualifier.Application.Database.Scope.Queries.GetAllScopesByStandardId;
using Qualifier.Application.Database.Scope.Queries.GetScopeById;
using Qualifier.Application.Database.Scope.Queries.GetScopesByStandardId;
using Qualifier.Common.Api;
using Qualifier.Common.Application.Dto;

namespace Qualifier.Api.Scopelers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ScopeController : ControllerBase
    {
        [HttpGet("All")]
        public async Task<IActionResult> GetAll(int standardId, [FromServices] IGetAllScopesByStandardIdQuery query)
        {

            var res = await query.Execute(standardId);
            if (res.GetType() == typeof(BaseErrorResponseDto))
                return BadRequest(res);
            else
                return Ok(res);
        }

        [HttpGet()]
        public async Task<IActionResult> Get(int skip, int pageSize, string? search, int standardId, [FromServices] IGetScopesByStandardIdQuery query)
        {
            if (search == null)
                search = string.Empty;

            var res = await query.Execute(skip, pageSize, search, standardId);
            if (res.GetType() == typeof(BaseErrorResponseDto))
                return BadRequest(res);
            else
                return Ok(res);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id, [FromServices] IGetScopeByIdQuery getScopeByIdQuery)
        {
            var res = await getScopeByIdQuery.Execute(id);
            if (res.GetType() == typeof(BaseErrorResponseDto))

                return BadRequest(res);
            else
                return Ok(new
                {
                    data = res
                });
        }

        [HttpPost()]
        public async Task<IActionResult> Create([FromBody] CreateScopeDto model, [FromServices] ICreateScopeCommand createScopeCommand)
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

            var res = await createScopeCommand.Execute(model);
            if (res.GetType() == typeof(BaseErrorResponseDto))
                return BadRequest(res);
            else

                return Ok(new
                {
                    data = res
                });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] UpdateScopeDto model, int id, [FromServices] IUpdateScopeCommand updateScopeCommand)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            int userId;

            bool success = int.TryParse(JwtTokenProvider.GetUserIdFromToken(accessToken), out userId);

            if (success)
                model.updateUserId = userId;

            var res = await updateScopeCommand.Execute(model, id);
            if (res.GetType() == typeof(BaseErrorResponseDto))
                return BadRequest(res);
            else
                return Ok(new
                {
                    data = res
                });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> delete(int id, [FromServices] IDeleteScopeCommand deleteScopeCommand)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            int userIdValue;

            bool success = int.TryParse(JwtTokenProvider.GetUserIdFromToken(accessToken), out userIdValue);

            int userId = 0;
            if (success)
                userId = userIdValue;

            var res = await deleteScopeCommand.Execute(id, userId);
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
