using Qualifier.Common.Application.NotificationPattern;

namespace Qualifier.Application.Database.Menu.Commands.UpdateMenu
{
    public class UpdateMenuDto
    {
        public int menuId { get; set; }
        public string name { get; set; }
        public string? image { get; set; }
        public int? updateUserId { get; set; }

        public void requiredFieldsValidation(Notification notification)
        {
            if (name == null)
                notification.addError("El name es obligatorio");

        }

    }
}

