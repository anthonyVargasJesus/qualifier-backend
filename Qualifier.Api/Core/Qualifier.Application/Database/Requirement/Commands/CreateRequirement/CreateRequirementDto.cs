using Qualifier.Common.Application.NotificationPattern;

namespace Qualifier.Application.Database.Requirement.Commands.CreateRequirement
{
    public class CreateRequirementDto
    {
        public int? numeration { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int? standardId { get; set; }
        public int? level { get; set; }
        public int parentId { get; set; }
        public bool isEvaluable { get; set; }
        public int companyId { get; set; }
        public int? creationUserId { get; set; }
        public string letter { get; set; }
        public void requiredFieldsValidation(Notification notification)
        {
            if (numeration == null)
                notification.addError("El numeration es obligatorio");

            if (name == null)
                notification.addError("El name es obligatorio");

            if (standardId == null)
                notification.addError("El standardId es obligatorio");

            if (level == null)
                notification.addError("El level es obligatorio");


        }

    }
}

