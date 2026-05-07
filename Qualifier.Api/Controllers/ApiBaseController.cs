using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Qualifier.Common.Application.Dto;

namespace Qualifier.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class ApiBaseController : ControllerBase
    {
        // Propiedades calculadas para obtener datos del usuario actual
        protected int UserId => GetClaimAsInt("userId");
        protected int CompanyId => GetClaimAsInt("companyId");

        private int GetClaimAsInt(string claimType)
        {
            var claimValue = User.FindFirstValue(claimType);
            return int.TryParse(claimValue, out var result) ? result : 0;
        }

        // Centraliza la lógica de respuesta para evitar repetir ifs en cada endpoint
        protected IActionResult ProcessResponse(object response, bool wrapWithData = false)
        {
            if (response is BaseErrorResponseDto errorRes)
                return BadRequest(errorRes);

            if (wrapWithData)
                return Ok(new { data = response });

            return Ok(response);
        }

        protected IActionResult CompanyRequiredError() =>
            BadRequest(new { message = "El usuario no está asociado a una institución" });
    }
}
