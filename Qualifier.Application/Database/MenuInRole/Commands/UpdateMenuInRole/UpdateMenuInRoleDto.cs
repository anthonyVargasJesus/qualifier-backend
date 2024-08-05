using Qualifier.Common.Application.NotificationPattern;

namespace Qualifier.Application.Database.MenuInRole.Commands.UpdateMenuInRole
{
    public class UpdateMenuInRoleDto
    {
        public int menuInRoleId { get; set; }
        public int menuId { get; set; }
        public int roleId { get; set; }
        public int order { get; set; }
        public int? updateUserId { get; set; }

        public void requiredFieldsValidation(Notification notification)
        {
            if (menuId == null)
                notification.addError("El menuId es obligatorio");

            if (roleId == null)
                notification.addError("El roleId es obligatorio");

            if (order == null)
                notification.addError("El order es obligatorio");

        }

    }
}

