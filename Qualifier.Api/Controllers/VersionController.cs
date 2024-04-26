using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Qualifier.Application.Database.Evaluation.Queries.GetExcelDashboard;
using Qualifier.Application.Database.Version.Commands.CreateVersion;
using Qualifier.Application.Database.Version.Commands.CreateWordDocumento;
using Qualifier.Application.Database.Version.Commands.DeleteVersion;
using Qualifier.Application.Database.Version.Commands.UpdateVersion;
using Qualifier.Application.Database.Version.Queries.GetVersionById;
using Qualifier.Application.Database.Version.Queries.GetVersionsByDocumentationId;
using Qualifier.Common.Api;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.NotificationPattern;


namespace Qualifier.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VersionController : ControllerBase
    {


        [HttpGet]
        [Route("word-document")]
        public async Task<FileResult> GetDashboardExcelAsync(int versionId, [FromServices] ICreateWordDocumentCommand query)
        {

            MemoryStream ms = new MemoryStream();
            ms = (MemoryStream)await query.Execute(versionId);
            string fileName = @"version.docx";
            return File(ms, "application/vnd.openxmlformats-officedocument.wordprocessingml.document.docx", fileName);
        }

        [HttpGet()]
        public async Task<IActionResult> Get(int skip, int pageSize, string? search, int documentationId, [FromServices] IGetVersionsByDocumentationIdQuery query)
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
        public async Task<IActionResult> Get(int id, [FromServices] IGetVersionByIdQuery getVersionByIdQuery)
        {
            var res = await getVersionByIdQuery.Execute(id);
            if (res.GetType() == typeof(BaseErrorResponseDto))

                return BadRequest(res);
            else
                return Ok(new
                {
                    data = res
                });
        }

        [HttpPost()]
        public async Task<IActionResult> Create([FromBody] CreateVersionDto model, [FromServices] ICreateVersionCommand createVersionCommand)
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

            var res = await createVersionCommand.Execute(model);
            if (res.GetType() == typeof(BaseErrorResponseDto))
                return BadRequest(res);
            else
                return Ok(new
                {
                    data = res
                });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] UpdateVersionDto model, int id, [FromServices] IUpdateVersionCommand updateVersionCommand)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            int userId;

            bool success = int.TryParse(JwtTokenProvider.GetUserIdFromToken(accessToken), out userId);

            if (success)
                model.updateUserId = userId;

            var res = await updateVersionCommand.Execute(model, id);
            if (res.GetType() == typeof(BaseErrorResponseDto))
                return BadRequest(res);
            else
                return Ok(new
                {
                    data = res
                });
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> delete(int id, [FromServices] IDeleteVersionCommand deleteVersionCommand)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            int userIdValue;

            bool success = int.TryParse(JwtTokenProvider.GetUserIdFromToken(accessToken), out userIdValue);

            int userId = 0;
            if (success)
                userId = userIdValue;

            var res = await deleteVersionCommand.Execute(id, userId);
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
