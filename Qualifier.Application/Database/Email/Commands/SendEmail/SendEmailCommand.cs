using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Pangolin.Application.Database.Email.Commands.SendEmail
{
    public class SendEmailCommand: ISendEmailCommand
    {
        public Object Execute(string toEmail, string subject)
        {
            try
            {
                // Set up SMTP client
                SmtpClient client2 = new SmtpClient("smtp.gmail.com", 465);
                client2.EnableSsl = true;
                client2.UseDefaultCredentials = false;
                client2.Credentials = new NetworkCredential("pangolin.sender.82@gmail.com", "umul gitr dzuj wgno");

                // Create email message
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("pangolin.sender.82@gmail.com");
                mailMessage.To.Add(toEmail);
                mailMessage.Subject = subject;
                mailMessage.IsBodyHtml = true;
                StringBuilder mailBody2 = new StringBuilder();
                mailBody2.AppendFormat("<h1>User Registered</h1>");
                mailBody2.AppendFormat("<br />");
                mailBody2.AppendFormat("<p>Thank you For Registering account</p>");
                mailMessage.Body = mailBody2.ToString();

                // Send email
                client2.Send(mailMessage);

                var client = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential("pangolin.sender.82@gmail.com", "umul gitr dzuj wgno"),
                    EnableSsl = true
                };

                StringBuilder mailBody = new StringBuilder();
                mailBody.AppendFormat("<h1>User Registered</h1>");
                mailBody.AppendFormat("<br />");
                mailBody.AppendFormat("<p>Thank you For Registering account</p>");

                client.Send("youremail@gmail.com", toEmail, subject, mailBody.ToString());


                BaseResponseCommandDto baseResponseCommandDto = new BaseResponseCommandDto();
                baseResponseCommandDto.response = "¡Correo enviado!";
                return baseResponseCommandDto;

            } catch(Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }

        }

    }
}

