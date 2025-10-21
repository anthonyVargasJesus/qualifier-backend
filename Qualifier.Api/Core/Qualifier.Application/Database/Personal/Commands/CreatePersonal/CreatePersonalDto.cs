using Qualifier.Common.Application.NotificationPattern;

namespace Qualifier.Application.Database.Personal.Commands.CreatePersonal
{
    public class CreatePersonalDto
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
        public int companyId { get; set; }
        public int? creationUserId { get; set; }

        public void requiredFieldsValidation(Notification notification)
        {
            if (name == null)
                notification.addError("El name es obligatorio");

            if (firstName == null)
                notification.addError("El firstName es obligatorio");

        }

    }
}

