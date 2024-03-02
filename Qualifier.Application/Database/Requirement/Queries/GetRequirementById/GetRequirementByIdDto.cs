namespace Qualifier.Application.Database.Requirement.Queries.GetRequirementById
{
    public class GetRequirementByIdDto
    {
        public int requirementId { get; set; }
        public int numeration { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int standardId { get; set; }
        public int level { get; set; }
        public int parentId { get; set; }
        public bool isEvaluable { get; set; }

    }
}

