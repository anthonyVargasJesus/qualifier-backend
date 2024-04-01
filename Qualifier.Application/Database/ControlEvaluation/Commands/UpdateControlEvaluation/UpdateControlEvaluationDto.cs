using Qualifier.Common.Application.NotificationPattern;
using static Qualifier.Application.Database.ControlEvaluation.Commands.CreateControlEvaluation.CreateControlEvaluationDto;

namespace Qualifier.Application.Database.ControlEvaluation.Commands.UpdateControlEvaluation
{
    public class UpdateControlEvaluationDto
    {
        public long controlEvaluationId { get; set; }
        public int controlId { get; set; }
        public int maturityLevelId { get; set; }
        public decimal value { get; set; }
        public int responsibleId { get; set; }
        public string justification { get; set; }
        public string improvementActions { get; set; }
        public string? controlDescription { get; set; }
        public string? controlType { get; set; }
        public int? updateUserId { get; set; }
        public int companyId { get; set; }
        public List<UpdateControlEvaluationReferenceDocumentationDto>? referenceDocumentations { get; set; }
        public void requiredFieldsValidation(Notification notification)
        {
            if (controlEvaluationId == null)
                notification.addError("El controlEvaluationId es obligatorio");

            if (controlId == null)
                notification.addError("El controlId es obligatorio");

            if (maturityLevelId == null)
                notification.addError("El maturityLevelId es obligatorio");

            if (value == null)
                notification.addError("El value es obligatorio");

            if (responsibleId == null)
                notification.addError("El responsibleId es obligatorio");

            if (justification == null)
                notification.addError("El justification es obligatorio");

            if (improvementActions == null)
                notification.addError("El ImprovementActions es obligatorio");


        }

        public class UpdateControlEvaluationReferenceDocumentationDto
        {
            public string name { get; set; }
            public int documentationId { get; set; }
            public long controlEvaluationId { get; set; }
            public int evaluationId { get; set; }
            public int companyId { get; set; }
            public int? creationUserId { get; set; }
        }

    }
}

