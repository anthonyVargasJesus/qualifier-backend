using Qualifier.Common.Application.NotificationPattern;

namespace Qualifier.Application.Database.Reviewer.Commands.CreateReviewer
{
    public class CreateReviewerDto
    {
        public int reviewerId { get; set; }
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

