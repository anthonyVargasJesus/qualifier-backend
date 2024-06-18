using Qualifier.Common.Application.NotificationPattern;

namespace Qualifier.Application.Database.OptionInMenu.Commands.UpdateOptionInMenu
{
    public class UpdateOptionInMenuDto
    {
        public int optionInMenuId { get; set; }
        public int optionId { get; set; }
        public int order { get; set; }
        public int? updateUserId { get; set; }

        public void requiredFieldsValidation(Notification notification)
        {

            if (optionId == null)
                notification.addError("El optionId es obligatorio");

            if (order == null)
                notification.addError("El order es obligatorio");

        }

    }
}

