using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Qualifier.Application.Database.Approver.Commands.CreateApprover;
using Qualifier.Application.Database.Approver.Commands.DeleteApprover;
using Qualifier.Application.Database.Approver.Commands.UpdateApprover;
using Qualifier.Application.Database.Approver.Queries.GetApproverById;
using Qualifier.Application.Database.Approver.Queries.GetAllApproversByVersionId;
using Qualifier.Common.Api;
using Qualifier.Common.Application.Dto;


namespace Qualifier.Api.Controllers
{
    [Route("api/[controller]")]

    [ApiController]
    public class ApproverController : ControllerBase
    {

        [HttpGet("All")]
        public async Task<IActionResult> Get(int versionId, [FromServices] IGetAllApproversByVersionIdQuery query)
        {
            var res = await query.Execute(versionId);
            if (res.GetType() == typeof(BaseErrorResponseDto))
                return BadRequest(res);
            else
                return Ok(res);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id, [FromServices] IGetApproverByIdQuery getApproverByIdQuery)
        {
            var res = await getApproverByIdQuery.Execute(id);
            if (res.GetType() == typeof(BaseErrorResponseDto))

                return BadRequest(res);
            else
                return Ok(new
                {
                    data = res
                });
        }

        [HttpPost()]
        public async Task<IActionResult> Create([FromBody] CreateApproverDto model, [FromServices] ICreateApproverCommand createApproverCommand)
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

            var res = await createApproverCommand.Execute(model);
            if (res.GetType() == typeof(BaseErrorResponseDto))
                return BadRequest(res);
            else

                return Ok(new
                {

                    data = res
                });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] UpdateApproverDto model, int id, [FromServices] IUpdateApproverCommand updateApproverCommand)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            int userId;

            bool success = int.TryParse(JwtTokenProvider.GetUserIdFromToken(accessToken), out userId);

            if (success)
                model.updateUserId = userId;

            var res = await updateApproverCommand.Execute(model, id);
            if (res.GetType() == typeof(BaseErrorResponseDto))
                return BadRequest(res);
            else
                return Ok(new
                {
                    data = res
                });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> delete(int id, [FromServices] IDeleteApproverCommand deleteApproverCommand)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            int userIdValue;

            bool success = int.TryParse(JwtTokenProvider.GetUserIdFromToken(accessToken), out userIdValue);

            int userId = 0;
            if (success)
                userId = userIdValue;

            var res = await deleteApproverCommand.Execute(id, userId);
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
