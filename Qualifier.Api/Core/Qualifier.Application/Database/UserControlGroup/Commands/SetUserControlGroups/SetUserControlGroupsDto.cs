using Qualifier.Common.Application.NotificationPattern;

namespace Qualifier.Application.Database.UserControlGroup.Commands.SetUserControlGroups
{
    public class SetUserControlGroupsDto
    {
        public int userId { get; set; }
        public int standardId { get; set; }
        public int companyId { get; set; }
        public int? creationUserId { get; set; }
        public List<int> controlGroupIds { get; set; } = new();

        public void requiredFieldsValidation(Notification notification)
        {
            if (userId <= 0)
                notification.addError("El userId es obligatorio");

            if (standardId <= 0)
                notification.addError("El standardId es obligatorio");
        }
    }
}
