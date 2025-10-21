using Qualifier.Common.Application.NotificationPattern;

namespace Qualifier.Application.Database.Scope.Commands.UpdateScope
{
    public class UpdateScopeDto
    {
        public int scopeId { get; set; }
        public bool isCurrent { get; set; }
        public DateTime date { get; set; }
        public string name { get; set; }
        public string? description { get; set; }
        public int? standardId { get; set; }
        public int? updateUserId { get; set; }

        public void requiredFieldsValidation(Notification notification)
        {
            if (isCurrent == null)
                notification.addError("El isCurrent es obligatorio");

            if (date == null)
                notification.addError("El date es obligatorio");

            if (name == null)
                notification.addError("El name es obligatorio");

        }

    }
}

