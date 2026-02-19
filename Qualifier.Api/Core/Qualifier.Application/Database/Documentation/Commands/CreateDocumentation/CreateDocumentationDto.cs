using System.Configuration;
using Qualifier.Common.Application.NotificationPattern;

namespace Qualifier.Application.Database.Documentation.Commands.CreateDocumentation
{
    public class CreateDocumentationDto
    {
        public int? documentationId { get; set; }
        public string? name { get; set; }
        public string? description { get; set; }
        public string? template { get; set; }
        public int? documentTypeId { get; set; }
        public int? standardId { get; set; }
        public int? companyId { get; set; }
        public int? creationUserId { get; set; }

        public void requiredFieldsValidation(Notification notification)
        {
            if (name == null)
                notification.addError("El name es obligatorio");

            if (standardId == null && standardId <= 0)
                notification.addError("El standardId es obligatorio");

            if (companyId == null && companyId <= 0)
                notification.addError("El companyId es obligatorio");

        }

    }
}

