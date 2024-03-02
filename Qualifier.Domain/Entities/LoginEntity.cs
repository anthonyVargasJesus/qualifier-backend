using Qualifier.Common.Application.NotificationPattern;
using Qualifier.Domain.Enumerations;

namespace Qualifier.Domain.Entities
{
    public class LoginEntity
    {
        public int userId { get; set; }
        public string? name { get; set; }
        public string? email { get; set; }
        public string? password { get; set; }
        public UserStateEntity? userState { get; set; }
        public List<RoleEntity>? roles { get; set; }
        public List<MenuEntity>? menus { get; set; }
        public string? token { get; set; }
        public void validation(Notification notification)
        {
            this.validateState(notification);
            this.validateRoles(notification);
        }

        private void validateRoles(Notification notification)
        {
            if (roles == null)
            {
                notification.addError("El usuario no tiene ningún rol asignado");
                return;
            }
            if (roles.Count == 0)
            {
                notification.addError("El usuario no tiene ningún rol asignado");
                return;
            }
        }

        private void validateState(Notification notification)
        {
            if (userState == null)
            {
                notification.addError("El usuario no está activo en el sistema 1");
                return;
            }

            if (userState.value != (int) EnumUserState.Active)
            {
                notification.addError("El usuario no está activo en el sistema 2");
                return;
            }
        }

        public void setMenus(List<RoleEntity> roles)
        {
            this.menus = new List<MenuEntity>();
            foreach (RoleEntity role in roles)
            {
                if (role.menus != null)
                foreach (MenuEntity menuByRole in role.menus)
                {
                    int counter = 0;
                    foreach (MenuEntity menu in this.menus)
                        if (menu.menuId == menuByRole.menuId)
                            counter++;
                    if (counter == 0)
                        this.menus.Add(menuByRole);
                }
            }

        }

    }
}
