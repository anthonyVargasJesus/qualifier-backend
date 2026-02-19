using Qualifier.Common.Application.NotificationPattern;

namespace Qualifier.Application.Database.Section.Commands.CreateSection
{
    public class CreateSectionDto
    {
        public int sectionId { get; set; }
        public int numeration { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int level { get; set; }
        public int parentId { get; set; }
        public int documentationId { get; set; }
        public int? versionId { get; set; }
        public int companyId { get; set; }
        public int? creationUserId { get; set; }
        public void requiredFieldsValidation(Notification notification)
        {
            if (numeration == null)
                notification.addError("El numeration es obligatorio");

            if (name == null)
                notification.addError("El name es obligatorio");

            if (level == null)
                notification.addError("El level es obligatorio");

            if (companyId == null)
                notification.addError("El companyId es obligatorio");

        }

    }
}

