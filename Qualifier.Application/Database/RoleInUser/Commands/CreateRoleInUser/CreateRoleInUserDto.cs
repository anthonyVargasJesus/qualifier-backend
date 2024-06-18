using Qualifier.Common.Application.NotificationPattern;

namespace Qualifier.Application.Database.RoleInUser.Commands.CreateRoleInUser
{
    public class CreateRoleInUserDto
    {
        public int roleInUserId { get; set; }
        public int roleId { get; set; }
        public int userId { get; set; }
        public int? companyId { get; set; }
        public int? creationUserId { get; set; }

        public void requiredFieldsValidation(Notification notification)
        {
            if (roleId == null)
                notification.addError("El roleId es obligatorio");

        }

    }
}

