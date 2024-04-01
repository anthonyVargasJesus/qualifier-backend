using Qualifier.Common.Application.NotificationPattern;

namespace Qualifier.Application.Database.Evaluation.Commands.UpdateEvaluation
{
    public class UpdateEvaluationDto
    {
        public DateTime startDate { get; set; }
        public DateTime? endDate { get; set; }
        public string description { get; set; }
        public int standardId { get; set; }
        public int? updateUserId { get; set; }

        public void requiredFieldsValidation(Notification notification)
        {
            if (startDate == null)
                notification.addError("El startDate es obligatorio");

        }

    }
}

