using Qualifier.Common.Application.NotificationPattern;

namespace Qualifier.Application.Database.ActiveType.Commands.CreateActiveType
{
    public class CreateActiveTypeDto
    {
        public string? name { get; set; }
        public int companyId { get; set; }
        public int? creationUserId { get; set; }

        public void requiredFieldsValidation(Notification notification)
        {
            if (name == null)
                notification.addError("El name es obligatorio");

        }

    }
}
