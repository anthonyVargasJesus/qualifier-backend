using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Qualifier.Application.Database.RiskAssessment.Commands.CreateRiskAssessment;
using Qualifier.Application.Database.RiskAssessment.Commands.UpdateRiskAssessment;
using Qualifier.Application.Database.RiskAssessment.Queries.GetRiskAssessmentById;
using Qualifier.Common.Api;
using Qualifier.Common.Application.Dto;

namespace Qualifier.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RiskAssessmentController : ControllerBase
    {

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id, [FromServices] IGetRiskAssessmentByIdQuery getRiskAssessmentByIdQuery)
        {
            var res = await getRiskAssessmentByIdQuery.Execute(id);
            if (res.GetType() == typeof(BaseErrorResponseDto))

                return BadRequest(res);
            else
                return Ok(new
                {
                    data = res
                });
        }

        [HttpPost()]
        public async Task<IActionResult> Create([FromBody] CreateRiskAssessmentDto model,
            [FromServices] ICreateRiskAssessmentCommand createRiskAssessmentCommand)
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

            var res = await createRiskAssessmentCommand.Execute(model);
            if (res.GetType() == typeof(BaseErrorResponseDto))
                return BadRequest(res);
            else
                return Ok(new
                {
                    data = res
                });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] UpdateRiskAssessmentDto model, int id,
            [FromServices] IUpdateRiskAssessmentCommand updateRiskAssessmentCommand)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            int userId;

            bool success = int.TryParse(JwtTokenProvider.GetUserIdFromToken(accessToken), out userId);

            if (success)
                model.updateUserId = userId;

            var res = await updateRiskAssessmentCommand.Execute(model, id);
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
