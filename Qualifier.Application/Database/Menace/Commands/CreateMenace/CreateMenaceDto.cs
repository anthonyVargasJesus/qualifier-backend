using Qualifier.Common.Application.NotificationPattern;

namespace Qualifier.Application.Database.Menace.Commands.CreateMenace
{
    public class CreateMenaceDto
    {
        public int menaceId { get; set; }
        public int menaceTypeId { get; set; }
        public string name { get; set; }
        public int? companyId { get; set; }
        public int? creationUserId { get; set; }

        public void requiredFieldsValidation(Notification notification)
        {
            if (menaceTypeId == null)
                notification.addError("El menaceTypeId es obligatorio");

            if (name == null)
                notification.addError("El name es obligatorio");

        }

    }
}

