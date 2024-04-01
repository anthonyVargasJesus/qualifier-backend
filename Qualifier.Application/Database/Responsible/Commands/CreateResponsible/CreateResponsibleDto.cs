using Qualifier.Common.Application.NotificationPattern;

namespace Qualifier.Application.Database.Responsible.Commands.CreateResponsible
{
    public class CreateResponsibleDto
    {
        public string name { get; set; }
        public string description { get; set; }
        public int standardId { get; set; }
        public int? companyId { get; set; }
        public int? creationUserId { get; set; }
        public void requiredFieldsValidation(Notification notification)
        {
            if (name == null)
                notification.addError("El name es obligatorio");

            if (companyId == null)
                notification.addError("El companyId es obligatorio");

        }

    }
}

