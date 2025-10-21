using Qualifier.Common.Application.NotificationPattern;

namespace Qualifier.Application.Database.Version.Commands.CreateVersion
{
    public class CreateVersionDto
    {
        public int versionId { get; set; }
        public decimal number { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public int confidentialityLevelId { get; set; }
        public int documentationId { get; set; }
        public DateTime date { get; set; }
        public bool isCurrent { get; set; }
        public string fileName { get; set; }
        public string description { get; set; }
        public int standardId { get; set; }
        public int companyId { get; set; }
        public int? creationUserId { get; set; }
        public int? versionReferenceId { get; set; }
        public void requiredFieldsValidation(Notification notification)
        {
            if (number == null)
                notification.addError("El number es obligatorio");

            if (code == null)
                notification.addError("El code es obligatorio");

            if (name == null)
                notification.addError("El name es obligatorio");

            if (confidentialityLevelId == null)
                notification.addError("El confidentialityLevelId es obligatorio");

            if (documentationId == null)
                notification.addError("El documentationId es obligatorio");

            if (date == null)
                notification.addError("El date es obligatorio");

            if (isCurrent == null)
                notification.addError("El isCurrent es obligatorio");

            if (standardId == null)
                notification.addError("El standardId es obligatorio");

        }

    }
}

