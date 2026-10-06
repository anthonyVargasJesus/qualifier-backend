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
        // StandardId (claim "standardId" del JWT) se eliminó a propósito: era la norma "fija"
        // del usuario al momento de loguearse, y quedaba desincronizada apenas la evaluación
        // actual pasaba a ser de otra norma (ver el bug que corrigió esto en
        // GetPlanDeAccionBootstrapQuery -- "Plan de acción" mostraba 0 brechas para NTP-42001
        // porque filtraba con la norma del token, ISO 27001). Cualquier endpoint que necesite la
        // norma correcta debe resolverla de la evaluación actual (evaluation.standardId) o de la
        // entidad puntual que esté consultando, nunca de acá. Si hace falta de nuevo, primero
        // hay que confirmar que no se vuelva a pisar con la norma real de lo que se está viendo.

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
