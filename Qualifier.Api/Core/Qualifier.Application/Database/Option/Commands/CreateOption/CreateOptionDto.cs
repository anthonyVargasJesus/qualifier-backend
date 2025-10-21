using Qualifier.Common.Application.NotificationPattern;

namespace Qualifier.Application.Database.Option.Commands.CreateOption
{
    public class CreateOptionDto
    {
        public int optionId { get; set; }
        public string name { get; set; }
        public string? image { get; set; }
        public string url { get; set; }
        public bool? isMobile { get; set; }
        public int? companyId { get; set; }
        public int? creationUserId { get; set; }

        public void requiredFieldsValidation(Notification notification)
        {
            if (name == null)
                notification.addError("El name es obligatorio");

            if (url == null)
                notification.addError("El url es obligatorio");

        }

    }
}

