using Qualifier.Common.Application.NotificationPattern;

namespace Qualifier.Application.Database.ReferenceDocumentation.Commands.CreateReferenceDocumentation
{
    public class CreateReferenceDocumentationDto
    {
        public string name { get; set; }
        public string url { get; set; }
        public string? description { get; set; }
        public int documentationId { get; set; }
        public int? requirementEvaluationId { get; set; }
        public int? controlEvaluationId { get; set; }
        public int? evaluationId { get; set; }
        public int? companyId { get; set; }
        public int? creationUserId { get; set; }

        public void requiredFieldsValidation(Notification notification)
        {
            if (name == null)
                notification.addError("El name es obligatorio");

            if (url == null)
                notification.addError("El url es obligatorio");

            if (documentationId == null)
                notification.addError("El documentationId es obligatorio");

        }

    }
}

