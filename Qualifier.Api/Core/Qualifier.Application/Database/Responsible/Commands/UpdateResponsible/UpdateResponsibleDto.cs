using Qualifier.Common.Application.NotificationPattern;

namespace Qualifier.Application.Database.Responsible.Commands.UpdateResponsible
{
    public class UpdateResponsibleDto
    {
        public string name { get; set; }
        public string description { get; set; }
        public int? updateUserId { get; set; }
        public void requiredFieldsValidation(Notification notification)
        {
            if (name == null)
                notification.addError("El name es obligatorio");

        }

    }
}

