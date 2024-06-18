using Qualifier.Common.Application.NotificationPattern;

namespace Qualifier.Application.Database.Menu.Commands.CreateMenu
{
    public class CreateMenuDto
    {
        public int menuId { get; set; }
        public string name { get; set; }
        public string? image { get; set; }
        public int? companyId { get; set; }
        public int? creationUserId { get; set; }

        public void requiredFieldsValidation(Notification notification)
        {
            if (name == null)
                notification.addError("El name es obligatorio");

        }

    }
}

