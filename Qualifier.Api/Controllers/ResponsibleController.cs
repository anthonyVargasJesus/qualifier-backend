using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Qualifier.Application.Database.Responsible.Commands.CreateResponsible;
using Qualifier.Application.Database.Responsible.Commands.DeleteResponsible;
using Qualifier.Application.Database.Responsible.Commands.UpdateResponsible;
using Qualifier.Application.Database.Responsible.Queries.GetAllResponsiblesByStandardId;
using Qualifier.Application.Database.Responsible.Queries.GetResponsibleById;
using Qualifier.Application.Database.Responsible.Queries.GetResponsiblesByStandardId;
using Qualifier.Common.Api;
using Qualifier.Common.Application.Dto;


namespace Qualifier.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResponsibleController : ControllerBase
    {

        [HttpGet("All")]
        public async Task<IActionResult> GetAll(int standardId, [FromServices] IGetAllResponsiblesByStandardIdQuery query)
        {
            var res = await query.Execute(standardId);
            if (res.GetType() == typeof(BaseErrorResponseDto))
                return BadRequest(res);
            else
                return Ok(res);
        }


        [HttpGet()]
        public async Task<IActionResult> Get(int skip, int pageSize, string? search, int standardId, [FromServices] IGetResponsiblesByStandardIdQuery query)
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
        public async Task<IActionResult> Get(int id, [FromServices] IGetResponsibleByIdQuery getResponsibleByIdQuery)
        {
            var res = await getResponsibleByIdQuery.Execute(id);
            if (res.GetType() == typeof(BaseErrorResponseDto))

                return BadRequest(res);
            else
                return Ok(new
                {
                    data = res
                });
        }

        [HttpPost()]
        public async Task<IActionResult> Create([FromBody] CreateResponsibleDto model, [FromServices] ICreateResponsibleCommand createResponsibleCommand)
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

            var res = await createResponsibleCommand.Execute(model);
            if (res.GetType() == typeof(BaseErrorResponseDto))
                return BadRequest(res);
            else

                return Ok(new
                {

                    data = res
                });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] UpdateResponsibleDto model, int id, [FromServices] IUpdateResponsibleCommand updateResponsibleCommand)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            int userId;

            bool success = int.TryParse(JwtTokenProvider.GetUserIdFromToken(accessToken), out userId);

            if (success)
                model.updateUserId = userId;

            var res = await updateResponsibleCommand.Execute(model, id);
            if (res.GetType() == typeof(BaseErrorResponseDto))
                return BadRequest(res);
            else
                return Ok(new
                {
                    data = res
                });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> delete(int id, [FromServices] IDeleteResponsibleCommand deleteResponsibleCommand)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            int userIdValue;

            bool success = int.TryParse(JwtTokenProvider.GetUserIdFromToken(accessToken), out userIdValue);

            int userId = 0;
            if (success)
                userId = userIdValue;

            var res = await deleteResponsibleCommand.Execute(id, userId);
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
