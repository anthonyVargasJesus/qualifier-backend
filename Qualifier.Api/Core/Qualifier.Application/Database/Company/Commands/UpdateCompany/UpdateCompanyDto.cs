using Qualifier.Common.Application.NotificationPattern;

namespace Qualifier.Application.Database.Company.Commands.UpdateCompany
{
    public class UpdateCompanyDto
    {
        public string name { get; set; }
        public string? abbreviation { get; set; }
        public string? slogan { get; set; }
        public string? logo { get; set; }
        public string? address { get; set; }
        public string? phone { get; set; }
        public int? updateUserId { get; set; }

        public void requiredFieldsValidation(Notification notification)
        {
            if (name == null)
                notification.addError("El name es obligatorio");

        }

    }
}

