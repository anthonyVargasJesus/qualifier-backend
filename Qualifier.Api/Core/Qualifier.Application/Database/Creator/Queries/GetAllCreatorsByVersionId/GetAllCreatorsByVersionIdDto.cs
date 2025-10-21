namespace Qualifier.Application.Database.Creator.Queries.GetAllCreatorsByVersionId
{
    public class GetAllCreatorsByVersionIdDto
    {
        public int creatorId { get; set; }
        public int personalId { get; set; }
        public int responsibleId { get; set; }
        public GetAllCreatorsByVersionIdPersonalDto? personal { get; set; }
        public GetAllCreatorsByVersionIdResponsibleDto? responsible { get; set; }
    }
    public class GetAllCreatorsByVersionIdPersonalDto
    {
        public string name { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }

    }
    public class GetAllCreatorsByVersionIdResponsibleDto
    {
        public string name { get; set; }

    }
}
