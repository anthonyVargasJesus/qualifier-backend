namespace Qualifier.Application.Database.SupportType.Queries.GetSupportTypesByCompanyId
{
    public class GetSupportTypesByCompanyIdDto
    {
        public int supportTypeId { get; set; }
        public string name { get; set; }

    }
}
//SupportType
//CreateMap<SupportTypeEntity, GetSupportTypesByCompanyIdDto>().ReverseMap();
