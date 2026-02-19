using Qualifier.Common.Application.NotificationPattern;

namespace Qualifier.Application.Database.Custodian.Commands.UpdateCustodian
{
    public class UpdateCustodianDto
    {
        public int? custodianId { get; set; }
        public string? code { get; set; }
        public string? name { get; set; }
        public int? updateUserId { get; set; }

        public void requiredFieldsValidation(Notification notification)
        {
            if (name == null)
                notification.addError("El name es obligatorio");

        }

    }
}

