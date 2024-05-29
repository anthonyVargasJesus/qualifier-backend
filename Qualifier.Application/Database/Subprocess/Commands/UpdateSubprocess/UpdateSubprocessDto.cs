using Qualifier.Common.Application.NotificationPattern;

namespace Qualifier.Application.Database.Subprocess.Commands.UpdateSubprocess
{
    public class UpdateSubprocessDto
    {
        public int subprocessId { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public int macroprocessId { get; set; }
        public int? updateUserId { get; set; }

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

