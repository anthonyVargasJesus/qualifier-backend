using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Qualifier.Api.Helpers;
using Qualifier.Application.Database.ReferenceDocumentation.Commands.CreateReferenceDocumentation;
using Qualifier.Application.Database.ReferenceDocumentation.Commands.DeleteReferenceDocumentation;
using Qualifier.Application.Database.ReferenceDocumentation.Commands.UpdateReferenceDocumentation;
using Qualifier.Application.Database.ReferenceDocumentation.Queries.GetReferenceDocumentationById;
using Qualifier.Application.Database.ReferenceDocumentation.Queries.GetReferenceDocumentationsByControlEvaluationId;
using Qualifier.Application.Database.ReferenceDocumentation.Queries.GetReferenceDocumentationsByRequirementEvaluationId;
using Qualifier.Common.Api;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.NotificationPattern;


namespace Qualifier.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReferenceDocumentationController : ControllerBase
    {

        [HttpGet()]
        public async Task<IActionResult> Get(int skip, int pageSize, int requirementEvaluationId, string? search, 
            [FromServices] IGetReferenceDocumentationsByRequirementEvaluationIdQuery query)
        {
            if (search == null)
                search = string.Empty;

            var res = await query.Execute(skip, pageSize, search, requirementEvaluationId);
            if (res.GetType() == typeof(BaseErrorResponseDto))
                return BadRequest(res);
            else
                return Ok(res);
        }

        [HttpGet("ByControl")]
        public async Task<IActionResult> GetByControl(int skip, int pageSize, int controlEvaluationId, string? search,
    [FromServices] IGetReferenceDocumentationsByControlEvaluationIdQuery query)
        {
            if (search == null)
                search = string.Empty;

            var res = await query.Execute(skip, pageSize, search, controlEvaluationId);
            if (res.GetType() == typeof(BaseErrorResponseDto))
                return BadRequest(res);
            else
                return Ok(res);
        }

        [HttpPost()]
        public async Task<IActionResult> Create([FromBody] CreateReferenceDocumentationDto model, 
            [FromServices] ICreateReferenceDocumentationCommand command)
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
        public async Task<IActionResult> Put([FromBody] UpdateReferenceDocumentationDto model, int id,
            [FromServices] IUpdateReferenceDocumentationCommand command)
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

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id, [FromServices] IGetReferenceDocumentationByIdQuery query)
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> delete(int id, [FromServices] IDeleteReferenceDocumentationCommand command)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            int companyId = HttpContext.GetCompanyIdAsync(accessToken);

            Notification notification = new Notification();
            if (companyId == CompanyConstants.NO_COMPANY_ASSOCIATED)
                notification.addError("El usuario no está asociado a institución");

            int userId = HttpContext.GetUserIdAsync(accessToken);

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
