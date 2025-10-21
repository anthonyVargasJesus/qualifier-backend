using Qualifier.Common.Application.NotificationPattern;

namespace Qualifier.Application.Database.Location.Commands.CreateLocation
{
    public class CreateLocationDto
    {
        public int locationId { get; set; }
        public string abbreviation { get; set; }
        public string name { get; set; }
        public int companyId { get; set; }
        public int? creationUserId { get; set; }

        public void requiredFieldsValidation(Notification notification)
        {
            if (name == null)
                notification.addError("El name es obligatorio");

        }

    }
}

