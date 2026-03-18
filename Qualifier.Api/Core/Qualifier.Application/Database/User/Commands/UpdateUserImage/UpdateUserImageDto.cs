using Qualifier.Common.Application.NotificationPattern;

namespace Qualifier.Api.Core.Qualifier.Application.Database.User.Commands.UpdateUserImage
{
    public class UpdateUserImageDto
    {
        public int userId { get; set; }
        public string? image { get; set; }

        public void requiredFieldsValidation(Notification notification)
        {
            if (image == null)
                notification.addError("La imagen es obligatoria");

        }

    }
}
