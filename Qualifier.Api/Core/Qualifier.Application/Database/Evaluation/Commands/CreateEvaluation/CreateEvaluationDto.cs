using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.Evaluation.Commands.CreateEvaluation
{
    public class CreateEvaluationDto
    {
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public string description { get; set; }
        public int standardId { get; set; }
        public int? referenceEvaluationId { get; set; }
        public bool? isGapAnalysis { get; set; }
        public int companyId { get; set; }
        public int? creationUserId { get; set; }
        public bool isCurrent { get; set; }
        public void requiredFieldsValidation(Notification notification)
        {
            if (startDate == null)
                notification.addError("El startDate es obligatorio");

        }

    }
}

