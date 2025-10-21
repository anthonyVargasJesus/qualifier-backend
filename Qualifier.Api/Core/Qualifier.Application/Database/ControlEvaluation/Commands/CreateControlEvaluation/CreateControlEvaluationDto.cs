using Qualifier.Application.Database.RequirementEvaluation.Commands.CreateRequirementEvaluation;
using Qualifier.Common.Application.NotificationPattern;

namespace Qualifier.Application.Database.ControlEvaluation.Commands.CreateControlEvaluation
{
    public class CreateControlEvaluationDto
    {
        public int evaluationId { get; set; }
        public int controlId { get; set; }
        public int maturityLevelId { get; set; }
        public decimal value { get; set; }
        public int responsibleId { get; set; }
        public string justification { get; set; }
        public string improvementActions { get; set; }
        public string controlDescription { get; set; }
        public string controlType { get; set; }
        public int standardId { get; set; }
        public int companyId { get; set; }
        public int? creationUserId { get; set; }
        public List<CreateControlEvaluationReferenceDocumentationDto>? referenceDocumentations { get; set; }

        public void requiredFieldsValidation(Notification notification)
        {
            if (evaluationId == null)
                notification.addError("El evaluationId es obligatorio");

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

            if (controlDescription == null)
                notification.addError("El controlDescription es obligatorio");

            if (controlType == null)
                notification.addError("El controlType es obligatorio");

            if (companyId == null)
                notification.addError("El companyId es obligatorio");

        }

        public class CreateControlEvaluationReferenceDocumentationDto
        {
            public string name { get; set; }
            public int documentationId { get; set; }
            public long controlEvaluationId { get; set; }
            public int companyId { get; set; }
            public int? creationUserId { get; set; }
        }

    }
}

