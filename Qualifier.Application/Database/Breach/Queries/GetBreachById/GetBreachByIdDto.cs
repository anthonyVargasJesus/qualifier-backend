namespace Qualifier.Application.Database.Breach.Queries.GetBreachById
{
    public class GetBreachByIdDto
    {
        public int breachId { get; set; }
        public int evaluationId { get; set; }
        public int standardId { get; set; }
        public string? type { get; set; }
        public int requirementId { get; set; }
        public int controlId { get; set; }
        public string? numerationToShow { get; set; }
        public string? title { get; set; }
        public string description { get; set; }
        public int breachSeverityId { get; set; }
        public int breachStatusId { get; set; }
        public int responsibleId { get; set; }
        public string? evidenceDescription { get; set; }
        public GetBreachsByIdControlDto? control { get; set; }
        public GetBreachsByIdRequirementDto? requirement { get; set; }
    }
    public class GetBreachsByIdControlDto
    {
        public int controlId { get; set; }
        public string name { get; set; }

    }
    public class GetBreachsByIdRequirementDto
    {
        public int requirementId { get; set; }
        public string name { get; set; }

    }
}

