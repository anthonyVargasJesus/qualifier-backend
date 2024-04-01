using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Qualifier.Application.Database.ControlEvaluation.Commands.CreateControlEvaluation;
using Qualifier.Application.Database.ControlEvaluation.Commands.DeleteControlEvaluation;
using Qualifier.Application.Database.ControlEvaluation.Commands.UpdateControlEvaluation;
using Qualifier.Application.Database.ControlEvaluation.Queries.GetControlEvaluationById;
using Qualifier.Application.Database.ControlEvaluation.Queries.GetControlEvaluationByProcess;
using Qualifier.Common.Api;
using Qualifier.Common.Application.Dto;

namespace Qualifier.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControlEvaluationController : ControllerBase
    {
        [HttpGet("all")]
        public async Task<IActionResult> Get(int standardId, int evaluationId, [FromServices] IGetControlEvaluationByProcessQuery query)
        {
            var res = await query.Execute(standardId, evaluationId);
            if (res.GetType() == typeof(BaseErrorResponseDto))
                return BadRequest(res);
            else
                return Ok(res);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id, [FromServices] IGetControlEvaluationByIdQuery getControlEvaluationByIdQuery)
        {
            var res = await getControlEvaluationByIdQuery.Execute(id);
            if (res.GetType() == typeof(BaseErrorResponseDto))

                return BadRequest(res);
            else
                return Ok(new
                {
                    data = res
                });
        }

        [HttpPost()]
        public async Task<IActionResult> Create([FromBody] CreateControlEvaluationDto model, [FromServices] ICreateControlEvaluationCommand createControlEvaluationCommand)
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

            var res = await createControlEvaluationCommand.Execute(model);
            if (res.GetType() == typeof(BaseErrorResponseDto))
                return BadRequest(res);
            else

                return Ok(new
                {
                    data = res
                });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] UpdateControlEvaluationDto model, int id, [FromServices] IUpdateControlEvaluationCommand updateControlEvaluationCommand)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            int userId;

            bool success = int.TryParse(JwtTokenProvider.GetUserIdFromToken(accessToken), out userId);

            if (success)
                model.updateUserId = userId;

            int companyId;
            bool success2 = int.TryParse(JwtTokenProvider.GetCompanyIdFromToken(accessToken), out companyId);

            if (success2)
                model.companyId = companyId;

            model.companyId = companyId;

            var res = await updateControlEvaluationCommand.Execute(model, id);
            if (res.GetType() == typeof(BaseErrorResponseDto))
                return BadRequest(res);
            else
                return Ok(new
                {
                    data = res
                });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> delete(int id, [FromServices] IDeleteControlEvaluationCommand deleteControlEvaluationCommand)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            int userIdValue;

            bool success = int.TryParse(JwtTokenProvider.GetUserIdFromToken(accessToken), out userIdValue);

            int userId = 0;
            if (success)
                userId = userIdValue;

            var res = await deleteControlEvaluationCommand.Execute(id, userId);
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
