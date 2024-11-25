using Microsoft.AspNetCore.Mvc;
using Pangolin.Application.Database.Email.Commands.SendEmail;
using Qualifier.Common.Application.Dto;

namespace Pangolin.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {

        [HttpPost("sendEmail")]
        public IActionResult sendEmail([FromBody] EmailSenderDto model, [FromServices] ISendEmailCommand command)
        {

            var res = command.Execute(model.toEmail, model.subject);
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
