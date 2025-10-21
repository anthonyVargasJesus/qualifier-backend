using Qualifier.Common.Application.NotificationPattern;

namespace Qualifier.Application.Database.OptionInMenuInRole.Commands.CreateOptionInMenuInRole
{
    public class CreateOptionInMenuInRoleDto
    {
        public int optionInMenuInRoleId { get; set; }
        public int optionId { get; set; }
        public int order { get; set; }
        public int menuId { get; set; }
        public int roleId { get; set; }
        public int? companyId { get; set; }
        public int? creationUserId { get; set; }

        public void requiredFieldsValidation(Notification notification)
        {
            if (optionId == null)
                notification.addError("El optionId es obligatorio");

            if (order == null)
                notification.addError("El order es obligatorio");

            if (menuId == null)
                notification.addError("El menuId es obligatorio");

            if (roleId == null)
                notification.addError("El roleId es obligatorio");

        }

    }
}

