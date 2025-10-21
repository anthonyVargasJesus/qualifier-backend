namespace Qualifier.Application.Database.Menace.Queries.GetAllMenacesByCompanyId
{
    public class GetAllMenacesByCompanyIdDto
    {
        public int menaceId { get; set; }
        public int menaceTypeId { get; set; }
        public string name { get; set; }
        public GetAllMenacesByCompanyIdMenaceTypeDto? menaceType { get; set; }
    }
    public class GetAllMenacesByCompanyIdMenaceTypeDto
    {
        public string name { get; set; }

    }
}

