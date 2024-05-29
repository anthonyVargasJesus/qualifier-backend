namespace Qualifier.Application.Database.SupportType.Queries.GetAllSupportTypesByCompanyId
{
    public class GetAllSupportTypesByCompanyIdDto
    {
        public int supportTypeId { get; set; }
        public string name { get; set; }

    }
}
//SupportType
//CreateMap<SupportTypeEntity, GetAllSupportTypesByCompanyIdDto>().ReverseMap();
