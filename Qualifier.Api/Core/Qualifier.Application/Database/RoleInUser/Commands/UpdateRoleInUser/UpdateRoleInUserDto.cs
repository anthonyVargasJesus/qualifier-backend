using Qualifier.Common.Application.NotificationPattern;

namespace Qualifier.Application.Database.RoleInUser.Commands.UpdateRoleInUser
{
    public class UpdateRoleInUserDto
    {
        public int roleInUserId { get; set; }
        public int roleId { get; set; }
        public int? updateUserId { get; set; }

        public void requiredFieldsValidation(Notification notification)
        {
            if (roleId == null)
                notification.addError("El roleId es obligatorio");

        }

    }
}

