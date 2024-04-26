using Qualifier.Common.Application.NotificationPattern;

namespace Qualifier.Application.Database.Creator.Commands.CreateCreator
{
    public class CreateCreatorDto
    {
        public int creatorId { get; set; }
        public int personalId { get; set; }
        public int responsibleId { get; set; }
        public int versionId { get; set; }
        public int documentationId { get; set; }
        public int companyId { get; set; }
        public int? creationUserId { get; set; }

        public void requiredFieldsValidation(Notification notification)
        {
            if (versionId == null)
                notification.addError("El versionId es obligatorio");

        }

    }
}

