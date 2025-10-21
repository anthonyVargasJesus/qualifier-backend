namespace Qualifier.Application.Database.Menace.Queries.GetMenacesByCompanyId
{
    public class GetMenacesByCompanyIdDto
    {
        public int menaceId { get; set; }
        public int menaceTypeId { get; set; }
        public string name { get; set; }
        public GetMenacesByCompanyIdMenaceTypeDto? menaceType { get; set; }
    }
    public class GetMenacesByCompanyIdMenaceTypeDto
    {
        public string name { get; set; }

    }
}

