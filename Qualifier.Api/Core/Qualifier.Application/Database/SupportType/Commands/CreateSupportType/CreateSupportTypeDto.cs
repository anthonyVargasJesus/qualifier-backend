using Qualifier.Common.Application.NotificationPattern;

namespace Qualifier.Application.Database.SupportType.Commands.CreateSupportType
{
    public class CreateSupportTypeDto
    {
        public int supportTypeId { get; set; }
        public string name { get; set; }
        public int companyId { get; set; }
        public int? creationUserId { get; set; }

        public void requiredFieldsValidation(Notification notification)
        {
        }

    }
}
//SupportType
//CreateMap<SupportTypeEntity, CreateSupportTypeDto>().ReverseMap();
