using Qualifier.Common.Application.NotificationPattern;

namespace Qualifier.Application.Database.Location.Commands.UpdateLocation
{
    public class UpdateLocationDto
    {
        public int locationId { get; set; }
        public string abbreviation { get; set; }
        public string name { get; set; }
        public int? updateUserId { get; set; }

        public void requiredFieldsValidation(Notification notification)
        {
            if (name == null)
                notification.addError("El name es obligatorio");

        }

    }
}

