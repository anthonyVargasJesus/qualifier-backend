using Qualifier.Common.Application.NotificationPattern;

namespace Qualifier.Application.Database.Subprocess.Commands.CreateSubprocess
{
    public class CreateSubprocessDto
    {
        public string code { get; set; }
        public string name { get; set; }
        public int macroprocessId { get; set; }
        public int companyId { get; set; }
        public int? creationUserId { get; set; }

        public void requiredFieldsValidation(Notification notification)
        {
            if (code == null)
                notification.addError("El code es obligatorio");

            if (name == null)
                notification.addError("El name es obligatorio");

            if (macroprocessId == null)
                notification.addError("El macroprocessId es obligatorio");

        }

    }
}

