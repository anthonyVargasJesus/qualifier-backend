using Microsoft.AspNetCore.Mvc;
using Qualifier.Application.Database.User.Commands.Login;
using Qualifier.Common.Application.Dto;

namespace Qualifier.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {


        [HttpPost()]
        public async Task<IActionResult> Login([FromBody] LoginUserLoginTryDto model, [FromServices] ILoginUserCommand command)
        {
            var res = await command.Execute(model);
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
