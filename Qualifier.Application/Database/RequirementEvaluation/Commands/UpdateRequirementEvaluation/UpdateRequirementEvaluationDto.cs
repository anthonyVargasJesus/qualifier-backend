using Qualifier.Application.Database.RequirementEvaluation.Commands.CreateRequirementEvaluation;
using Qualifier.Common.Application.NotificationPattern;

namespace Qualifier.Application.Database.RequirementEvaluation.Commands.UpdateRequirementEvaluation
{
    public class UpdateRequirementEvaluationDto
    {
        public long requirementEvaluationId { get; set; }
        public int? requirementId { get; set; }
        public int? maturityLevelId { get; set; }
        public decimal? value { get; set; }
        public int? responsibleId { get; set; }
        public string justification { get; set; }
        public string improvementActions { get; set; }
        public int? updateUserId { get; set; }
        public int companyId { get; set; }
        public List<UpdateRequirementEvaluationReferenceDocumentationDto>? referenceDocumentations { get; set; }
        public void requiredFieldsValidation(Notification notification)
        {

            if (requirementId == null)
                notification.addError("El requirementId es obligatorio");

            if (maturityLevelId == null)
                notification.addError("El maturityLevelId es obligatorio");

            if (value == null)
                notification.addError("El value es obligatorio");

            if (responsibleId == null)
                notification.addError("El responsibleId es obligatorio");

            //if (justification == null)
            //    notification.addError("El justification es obligatorio");

            //if (improvementActions == null)
            //    notification.addError("El improvementActions es obligatorio");

        }

    }

    public class UpdateRequirementEvaluationReferenceDocumentationDto
    {
        public string name { get; set; }
        public int documentationId { get; set; }
        public long requirementEvaluationId { get; set; }
        public int evaluationId { get; set; }
        public int companyId { get; set; }
        public int? creationUserId { get; set; }
    }

}

