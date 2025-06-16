using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Qualifier.Api.Helpers;
using Qualifier.Application.Database.DocumentType.Commands.CreateDocumentType;
using Qualifier.Application.Database.DocumentType.Commands.DeleteDocumentType;
using Qualifier.Application.Database.DocumentType.Commands.UpdateDocumentType;
using Qualifier.Application.Database.DocumentType.Queries.GetAllDocumentTypesByCompanyId;
using Qualifier.Application.Database.DocumentType.Queries.GetDocumentTypeById;
using Qualifier.Application.Database.DocumentType.Queries.GetDocumentTypesByCompanyId;
using Qualifier.Common.Api;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Common.Application.Service;

namespace Qualifier.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentTypeController : ControllerBase
    {
        [HttpGet("All")]
        public async Task<IActionResult> GetAll([FromServices] IGetAllDocumentTypesByCompanyIdQuery query)
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

        [HttpGet()]
        public async Task<IActionResult> Get(int skip, int pageSize, string? search, [FromServices] IGetDocumentTypesByCompanyIdQuery query)
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
        public async Task<IActionResult> Get(int id, [FromServices] IGetDocumentTypeByIdQuery getDocumentTypeByIdQuery)
        {
            var res = await getDocumentTypeByIdQuery.Execute(id);
            if (res.GetType() == typeof(BaseErrorResponseDto))

                return BadRequest(res);
            else
                return Ok(new
                {
                    data = res
                });
        }

        [HttpPost()]
        public async Task<IActionResult> Create([FromBody] CreateDocumentTypeDto model, [FromServices] ICreateDocumentTypeCommand createDocumentTypeCommand)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            int companyId = HttpContext.GetCompanyIdAsync(accessToken);

            Notification notification = new Notification();
            if (companyId == CompanyConstants.NO_COMPANY_ASSOCIATED)
                notification.addError("El usuario no está asociado a institución");
            else
                model.companyId = companyId;

            model.creationUserId = HttpContext.GetUserIdAsync(accessToken);

            var res = await createDocumentTypeCommand.Execute(model);
            if (res.GetType() == typeof(BaseErrorResponseDto))
                return BadRequest(res);
            else
                return Ok(new
                {
                    data = res
                });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] UpdateDocumentTypeDto model, int id, [FromServices] IUpdateDocumentTypeCommand updateDocumentTypeCommand)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            int companyId = HttpContext.GetCompanyIdAsync(accessToken);

            Notification notification = new Notification();
            if (companyId == CompanyConstants.NO_COMPANY_ASSOCIATED)
                notification.addError("El usuario no está asociado a institución");

            model.updateUserId = HttpContext.GetUserIdAsync(accessToken);

            var res = await updateDocumentTypeCommand.Execute(model, id);
            if (res.GetType() == typeof(BaseErrorResponseDto))
                return BadRequest(res);
            else
                return Ok(new
                {
                    data = res
                });
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> delete(int id, [FromServices] IDeleteDocumentTypeCommand deleteDocumentTypeCommand)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            int companyId = HttpContext.GetCompanyIdAsync(accessToken);

            Notification notification = new Notification();
            if (companyId == CompanyConstants.NO_COMPANY_ASSOCIATED)
                notification.addError("El usuario no está asociado a institución");

            int userId = HttpContext.GetUserIdAsync(accessToken);

            var res = await deleteDocumentTypeCommand.Execute(id, userId);
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
