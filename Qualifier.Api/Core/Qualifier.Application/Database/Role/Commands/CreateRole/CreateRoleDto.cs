using Qualifier.Common.Application.NotificationPattern;

namespace Qualifier.Application.Database.Role.Commands.CreateRole
{
    public class CreateRoleDto
    {
        public int roleId { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public int? creationUserId { get; set; }
        public int? companyId { get; set; }
        public void requiredFieldsValidation(Notification notification)
        {
            if (name == null)
                notification.addError("El name es obligatorio");

        }

    }
}

