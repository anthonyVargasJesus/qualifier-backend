using Qualifier.Common.Application.NotificationPattern;

namespace Qualifier.Application.Database.Creator.Commands.UpdateCreator
{
    public class UpdateCreatorDto
    {
        public int? creatorId { get; set; }
        public int? personalId { get; set; }
        public int? responsibleId { get; set; }
        public int? updateUserId { get; set; }

        public void requiredFieldsValidation(Notification notification)
        {
        }

    }
}

