using Qualifier.Common.Application.NotificationPattern;

namespace Qualifier.Application.Database.OptionInMenu.Commands.CreateOptionInMenu
{
    public class CreateOptionInMenuDto
    {
        public int optionInMenuId { get; set; }
        public int menuId { get; set; }
        public int optionId { get; set; }
        public int order { get; set; }
        public int? companyId { get; set; }
        public int? creationUserId { get; set; }

        public void requiredFieldsValidation(Notification notification)
        {
            if (menuId == null)
                notification.addError("El menuId es obligatorio");

            if (optionId == null)
                notification.addError("El optionId es obligatorio");

            if (order == null)
                notification.addError("El order es obligatorio");

        }

    }
}

