using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Qualifier.Api.Helpers;
using Qualifier.Application.Database.Evaluation.Commands.CreateEvaluation;
using Qualifier.Application.Database.Evaluation.Commands.DeleteEvaluation;
using Qualifier.Application.Database.Evaluation.Commands.UpdateEvaluation;
using Qualifier.Application.Database.Evaluation.Queries.GetAllEvaluationsByCompanyId;
using Qualifier.Application.Database.Evaluation.Queries.GetControlsDashboard;
using Qualifier.Application.Database.Evaluation.Queries.GetCurrentEvaluation;
using Qualifier.Application.Database.Evaluation.Queries.GetDashboard;
using Qualifier.Application.Database.Evaluation.Queries.GetEvaluationById;
using Qualifier.Application.Database.Evaluation.Queries.GetEvaluationsByCompanyId;
using Qualifier.Application.Database.Evaluation.Queries.GetExcelDashboard;
using Qualifier.Application.Database.Evaluation.Queries.GetPendingDocumentation;
using Qualifier.Common.Api;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;

namespace Qualifier.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EvaluationController : ControllerBase
    {

        [HttpGet("current")]
        public async Task<IActionResult> Get(int id, [FromServices] IGetCurrentEvaluationQuery query)
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

        [HttpGet("All")]
        public async Task<IActionResult> GetAll([FromServices] IGetAllEvaluationsByCompanyIdQuery query)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            int companyId = HttpContext.GetCompanyIdAsync(accessToken);

            Notification notification = new Notification();
            if (companyId == CompanyConstants.NO_COMPANY_ASSOCIATED)
                notification.addError("El usuario no está asociado a institución");

            if (notification.hasErrors())
                return BadRequest(BaseApplication.getApplicationErrorResponse(notification.errors));

            var res = await query.Execute(companyId);
            if (res.GetType() == typeof(BaseErrorResponseDto))
                return BadRequest(res);
            else
                return Ok(res);
        }


        [HttpGet]
        [Route("excel-dashboard")]
        public async Task<FileResult> GetDashboardExcelAsync(int standardId, int evaluationId,
            [FromServices] IGetExcelDashboardQuery query)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            int companyId = HttpContext.GetCompanyIdAsync(accessToken);

            Notification notification = new Notification();
            if (companyId == CompanyConstants.NO_COMPANY_ASSOCIATED)
                notification.addError("El usuario no está asociado a institución");

            MemoryStream ms = new MemoryStream();
            ms = (MemoryStream) await query.Execute(standardId, evaluationId, companyId);
            string fileName = @"Cotizacion.xlsx";
            return File(ms, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }


        [HttpGet("dashboard")]
        public async Task<IActionResult> Get(int standardId, int evaluationId, [FromServices] IGetDashboardQuery query)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            int companyId = HttpContext.GetCompanyIdAsync(accessToken);

            Notification notification = new Notification();
            if (companyId == CompanyConstants.NO_COMPANY_ASSOCIATED)
                notification.addError("El usuario no está asociado a institución");

            if (notification.hasErrors())
                return BadRequest(BaseApplication.getApplicationErrorResponse(notification.errors));

            var res = await query.Execute(standardId, evaluationId, companyId);
            if (res.GetType() == typeof(BaseErrorResponseDto))
                return BadRequest(res);
            else
                return Ok(res);
        }

        [HttpGet("control-dashboard")]
        public async Task<IActionResult> GetControlDashboard(int standardId, int evaluationId, [FromServices] IGetControlsDashboardQuery query)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            int companyId = HttpContext.GetCompanyIdAsync(accessToken);

            Notification notification = new Notification();
            if (companyId == CompanyConstants.NO_COMPANY_ASSOCIATED)
                notification.addError("El usuario no está asociado a institución");

            if (notification.hasErrors())
                return BadRequest(BaseApplication.getApplicationErrorResponse(notification.errors));

            var res = await query.Execute(standardId, evaluationId, companyId);
            if (res.GetType() == typeof(BaseErrorResponseDto))
                return BadRequest(res);
            else
                return Ok(res);
        }

        [HttpGet()]
        public async Task<IActionResult> Get(int skip, int pageSize, string? search, [FromServices] IGetEvaluationsByCompanyIdQuery query)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            int companyId = HttpContext.GetCompanyIdAsync(accessToken);

            Notification notification = new Notification();
            if (companyId == CompanyConstants.NO_COMPANY_ASSOCIATED)
                notification.addError("El usuario no está asociado a institución");

            if (notification.hasErrors())
                return BadRequest(BaseApplication.getApplicationErrorResponse(notification.errors));

            if (search == null)
                search = string.Empty;

            var res = await query.Execute(skip, pageSize, search, companyId);
            if (res.GetType() == typeof(BaseErrorResponseDto))
                return BadRequest(res);
            else
                return Ok(res);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id, [FromServices] IGetEvaluationByIdQuery getEvaluationByIdQuery)
        {
            var res = await getEvaluationByIdQuery.Execute(id);
            if (res.GetType() == typeof(BaseErrorResponseDto))

                return BadRequest(res);
            else
                return Ok(new
                {
                    data = res
                });
        }

        [HttpPost()]
        public async Task<IActionResult> Create([FromBody] CreateEvaluationDto model, [FromServices] ICreateEvaluationCommand createEvaluationCommand)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            int companyId = HttpContext.GetCompanyIdAsync(accessToken);

            Notification notification = new Notification();
            if (companyId == CompanyConstants.NO_COMPANY_ASSOCIATED)
                notification.addError("El usuario no está asociado a institución");
            else
                model.companyId = companyId;

            model.creationUserId = HttpContext.GetUserIdAsync(accessToken);

            int standardId;
            bool success3 = int.TryParse(JwtTokenProvider.GetStandardIdFromToken(accessToken), out standardId);

            if (success3)
                model.standardId = standardId;

            var res = await createEvaluationCommand.Execute(model);
            if (res.GetType() == typeof(BaseErrorResponseDto))
                return BadRequest(res);
            else
                return Ok(new
                {
                    data = res
                });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] UpdateEvaluationDto model, int id, [FromServices] IUpdateEvaluationCommand updateEvaluationCommand)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            int companyId = HttpContext.GetCompanyIdAsync(accessToken);

            Notification notification = new Notification();
            if (companyId == CompanyConstants.NO_COMPANY_ASSOCIATED)
                notification.addError("El usuario no está asociado a institución");
            else
                model.companyId = companyId;

            model.updateUserId = HttpContext.GetUserIdAsync(accessToken);

            var res = await updateEvaluationCommand.Execute(model, id);
            if (res.GetType() == typeof(BaseErrorResponseDto))
                return BadRequest(res);
            else
                return Ok(new
                {
                    data = res
                });
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> delete(int id, [FromServices] IDeleteEvaluationCommand deleteEvaluationCommand)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            int userId = HttpContext.GetUserIdAsync(accessToken);

            var res = await deleteEvaluationCommand.Execute(id, userId);
            if (res.GetType() == typeof(BaseErrorResponseDto))
                return BadRequest(res);
            else
                return Ok(new
                {
                    data = res
                });

        }

        [HttpGet("PendingDocumentation")]
        public async Task<IActionResult> GetPendingDocumentation(int skip, int pageSize, int standardId, int evaluationId, string? search, [FromServices] IGetPendingDocumentationQuery query)
        {
            if (search == null)
                search = string.Empty;

            var res = await query.Execute(skip, pageSize,search, standardId, evaluationId);
            if (res.GetType() == typeof(BaseErrorResponseDto))
                return BadRequest(res);
            else
                return Ok(res);
        }

    }
}
