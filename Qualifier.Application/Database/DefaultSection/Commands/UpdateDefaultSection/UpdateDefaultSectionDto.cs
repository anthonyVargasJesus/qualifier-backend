using Qualifier.Common.Application.NotificationPattern;

namespace Qualifier.Application.Database.DefaultSection.Commands.UpdateDefaultSection
{
    public class UpdateDefaultSectionDto
    {
        public int defaultSectionId { get; set; }
        public int numeration { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int level { get; set; }
        public int parentId { get; set; }
        public int? updateUserId { get; set; }

        public void requiredFieldsValidation(Notification notification)
        {
            if (defaultSectionId == null)
                notification.addError("El defaultSectionId es obligatorio");

            if (numeration == null)
                notification.addError("El numeration es obligatorio");

            if (name == null)
                notification.addError("El name es obligatorio");

            if (level == null)
                notification.addError("El level es obligatorio");

        }

    }
}

