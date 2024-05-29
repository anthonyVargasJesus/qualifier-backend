namespace Qualifier.Application.Database.SupportType.Queries.GetSupportTypeById
{
    public class GetSupportTypeByIdDto
    {
        public int supportTypeId { get; set; }
        public string name { get; set; }

    }
}
//SupportType
//CreateMap<SupportTypeEntity, GetSupportTypeByIdDto>().ReverseMap();
