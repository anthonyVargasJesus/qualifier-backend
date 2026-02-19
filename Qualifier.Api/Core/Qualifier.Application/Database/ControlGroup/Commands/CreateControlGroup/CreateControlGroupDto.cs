using Qualifier.Common.Application.NotificationPattern;

namespace Qualifier.Application.Database.ControlGroup.Commands.CreateControlGroup
{
    public class CreateControlGroupDto
    {
        public int? number { get; set; }
        public string? name { get; set; }
        public string? description { get; set; }
        public int standardId { get; set; }
        public int companyId { get; set; }
        public int? creationUserId { get; set; }
        public void requiredFieldsValidation(Notification notification)
        {
            if (number == null)
                notification.addError("El number es obligatorio");

            if (name == null)
                notification.addError("El name es obligatorio");

        }

    }
}

