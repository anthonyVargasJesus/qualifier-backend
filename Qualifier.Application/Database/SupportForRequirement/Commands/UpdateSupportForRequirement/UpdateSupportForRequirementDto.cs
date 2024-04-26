using Qualifier.Common.Application.NotificationPattern;

namespace Qualifier.Application.Database.SupportForRequirement.Commands.UpdateSupportForRequirement
{
    public class UpdateSupportForRequirementDto
    {
        public int supportForRequirementId { get; set; }
        public int documentationId { get; set; }
        public int requirementId { get; set; }
        public int? updateUserId { get; set; }

        public void requiredFieldsValidation(Notification notification)
        {
            if (documentationId == null)
                notification.addError("El documentationId es obligatorio");

            if (requirementId == null)
                notification.addError("El requirementId es obligatorio");

        }

    }
}

