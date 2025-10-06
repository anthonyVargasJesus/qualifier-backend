using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Qualifier.Application.Database.Breach.Commands.CreateBreach;
using Qualifier.Application.Database.Breach.Commands.DeleteBreach;
using Qualifier.Application.Database.Breach.Commands.UpdateBreach;
using Qualifier.Application.Database.Breach.Queries.GetBreachById;
using Qualifier.Application.Database.Breach.Queries.GetBreachsByEvaluationId;
using Qualifier.Application.Database.Breach.Queries.GetRisksIdentification;
using Qualifier.Common.Api;
using Qualifier.Common.Application.Dto;

namespace Qualifier.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BreachController : ControllerBase
    {

        [HttpGet("RisksIdentification")]
        public async Task<IActionResult> Get(int skip, int pageSize, string? search, [FromServices] IGetRisksIdentificationQuery query)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            int companyId;
            bool success2 = int.TryParse(JwtTokenProvider.GetCompanyIdFromToken(accessToken), out companyId);

            if (search == null)
                search = string.Empty;

            var res = await query.Execute(skip, pageSize, search, companyId);
            if (res.GetType() == typeof(BaseErrorResponseDto))
                return BadRequest(res);
            else
                return Ok(res);
        }

        [HttpGet()]
        public async Task<IActionResult> Get(int skip, int pageSize, string? search, int evaluationId, [FromServices] IGetBreachsByEvaluationIdQuery query)
        {
            if (search == null)
                search = string.Empty;

            var res = await query.Execute(skip, pageSize, search, evaluationId);
            if (res.GetType() == typeof(BaseErrorResponseDto))
                return BadRequest(res);
            else
                return Ok(res);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id, [FromServices] IGetBreachByIdQuery query)
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
        public async Task<IActionResult> Create([FromBody] CreateBreachDto model, [FromServices] ICreateBreachCommand command)
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
        public async Task<IActionResult> Put([FromBody] UpdateBreachDto model, int id, [FromServices] IUpdateBreachCommand command)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            int userId;

            bool success = int.TryParse(JwtTokenProvider.GetUserIdFromToken(accessToken), out userId);

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
        public async Task<IActionResult> delete(int id, [FromServices] IDeleteBreachCommand command)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            int userIdValue;

            bool success = int.TryParse(JwtTokenProvider.GetUserIdFromToken(accessToken), out userIdValue);

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
