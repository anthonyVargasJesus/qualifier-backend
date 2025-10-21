using Qualifier.Common.Application.NotificationPattern;

namespace Qualifier.Application.Database.Reviewer.Commands.UpdateReviewer
{
    public class UpdateReviewerDto
    {
        public int reviewerId { get; set; }
        public int personalId { get; set; }
        public int responsibleId { get; set; }
        public int? updateUserId { get; set; }

        public void requiredFieldsValidation(Notification notification)
        {
        }

    }
}

