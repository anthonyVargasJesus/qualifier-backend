using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pangolin.Application.Database.Email.Commands.SendEmail
{
    public interface ISendEmailCommand
    {
         Object Execute(string toEmail, string subject);
    }
}
