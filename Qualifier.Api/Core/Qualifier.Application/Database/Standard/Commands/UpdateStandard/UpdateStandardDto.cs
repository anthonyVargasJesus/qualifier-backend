using Qualifier.Common.Application.NotificationPattern;

namespace Qualifier.Application.Database.Standard.Commands.UpdateStandard
{
    public class UpdateStandardDto
    {
        public int standardId { get; set; }
        public string name { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;
        public int parentId { get; set; }
        public int companyId { get; set; }
        public int? updateUserId { get; set; }

        public void requiredFieldsValidation(Notification notification)
        {

            if (name == null)
                notification.addError("El nombre es obligatorio");

            if (description == null)
                notification.addError("La descripción es obligatorio");

        }

    }
}

