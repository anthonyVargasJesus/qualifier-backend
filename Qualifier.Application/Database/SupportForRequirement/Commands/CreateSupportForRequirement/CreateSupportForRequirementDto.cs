using Qualifier.Common.Application.NotificationPattern;

namespace Qualifier.Application.Database.SupportForRequirement.Commands.CreateSupportForRequirement
{
    public class CreateSupportForRequirementDto
    {
        public int supportForRequirementId { get; set; }
        public int documentationId { get; set; }
        public int requirementId { get; set; }
        public int standardId { get; set; }
        public int companyId { get; set; }
        public int? creationUserId { get; set; }

        public void requiredFieldsValidation(Notification notification)
        {
            if (documentationId == null)
                notification.addError("El documentationId es obligatorio");

            if (requirementId == null)
                notification.addError("El requirementId es obligatorio");

        }

    }
}

