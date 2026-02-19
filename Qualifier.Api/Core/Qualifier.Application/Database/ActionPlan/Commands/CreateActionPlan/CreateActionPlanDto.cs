using Qualifier.Common.Application.NotificationPattern;

namespace Qualifier.Application.Database.ActionPlan.Commands.CreateActionPlan
{
    public class CreateActionPlanDto
    {
        public int? actionPlanId { get; set; }
        public int? breachId { get; set; }
        public int? evaluationId { get; set; }
        public int? standardId { get; set; }
        public string? title { get; set; }
        public string? description { get; set; }
        public int? responsibleId { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? dueDate { get; set; }
        public int? actionPlanPriorityId { get; set; }
        public int? companyId { get; set; }
        public int? creationUserId { get; set; }

        public void requiredFieldsValidation(Notification notification)
        {
            if (breachId == null || breachId <= 0)
                notification.addError("El breachId no puede ser 0");

            if (evaluationId == null || evaluationId <= 0)
                notification.addError("El evaluationId no puede ser 0");

            if (standardId == null || standardId <= 0)
                notification.addError("El standardId no puede ser 0");

            if (string.IsNullOrWhiteSpace(title))
                notification.addError("El title es obligatorio");

            if (responsibleId == null || responsibleId <= 0)
                notification.addError("El responsibleId no puede ser 0");

            if (startDate == null)
                notification.addError("El startDate es obligatorio");

            if (dueDate == null)
                notification.addError("El dueDate es obligatorio");

            if (actionPlanPriorityId == null || actionPlanPriorityId <= 0)
                notification.addError("El actionPlanPriorityId no puede ser 0");

        }

    }
}

