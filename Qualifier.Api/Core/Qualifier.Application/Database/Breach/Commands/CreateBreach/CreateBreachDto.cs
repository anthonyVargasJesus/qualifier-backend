using System;
using Qualifier.Common.Application.NotificationPattern;

namespace Qualifier.Application.Database.Breach.Commands.CreateBreach
{
    public class CreateBreachDto
    {
        public int? breachId { get; set; }
        public int? evaluationId { get; set; }
        public int? standardId { get; set; }
        public string? type { get; set; }
        public int? requirementId { get; set; }
        public int? controlId { get; set; }
        public string? title { get; set; }
        public string? description { get; set; }
        public int? breachSeverityId { get; set; }
        public int? responsibleId { get; set; }
        public string? evidenceDescription { get; set; }
        public int? companyId { get; set; }
        public int? creationUserId { get; set; }
        public string? numerationToShow { get; set; }
        public void requiredFieldsValidation(Notification notification)
        {
            if (evaluationId == null && evaluationId <= 0)
                notification.addError("El evaluationId es obligatorio");

            if (standardId == null && standardId <= 0)
                notification.addError("El standardId es obligatorio");

            if (requirementId == null && requirementId <= 0)
                notification.addError("El requirementId es obligatorio");

            if (controlId == null && controlId <= 0)
                notification.addError("El controlId es obligatorio");

            if (description == null)
                notification.addError("El description es obligatorio");

            if (breachSeverityId == null && breachSeverityId <= 0)
                notification.addError("El breachSeverityId es obligatorio");

            if (responsibleId == null && responsibleId <= 0)
                notification.addError("El responsibleId es obligatorio");

        }

    }
}

