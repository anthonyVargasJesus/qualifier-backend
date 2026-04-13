using Qualifier.Common.Application.NotificationPattern;

namespace Qualifier.Application.Database.SupportType.Commands.UpdateSupportType
{
    public class UpdateSupportTypeDto
    {
        public int supportTypeId { get; set; }
        public string name { get; set; } = string.Empty;
        public int? updateUserId { get; set; }

        public void requiredFieldsValidation(Notification notification)
        {
        }

    }
}
