using Qualifier.Common.Application.NotificationPattern;

namespace Qualifier.Application.Database.Personal.Commands.UpdatePersonal
{
    public class UpdatePersonalDto
    {
        public int personalId { get; set; }
        public string name { get; set; }
        public string middleName { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string position { get; set; }
        public string image { get; set; }
        public int? updateUserId { get; set; }

        public void requiredFieldsValidation(Notification notification)
        {
            if (name == null)
                notification.addError("El name es obligatorio");

            if (firstName == null)
                notification.addError("El firstName es obligatorio");

        }

    }
}

