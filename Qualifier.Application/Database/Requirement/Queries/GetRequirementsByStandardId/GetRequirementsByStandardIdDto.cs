namespace Qualifier.Application.Database.Requirement.Queries.GetRequirementsByStandardId
{
    public class GetRequirementsByStandardIdDto
    {
        public int requirementId { get; set; }
        public int numeration { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int level { get; set; }
        public int parentId { get; set; }
        public string letter { get; set; }
        public List<GetRequirementsByStandardIdDto> children { get; set; }
        public string numerationToShow { get; set; }
    }
}

