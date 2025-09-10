namespace Qualifier.Application.Database.Breach.Queries.GetBreachsByEvaluationId
{
    public class GetBreachsByEvaluationIdDto
    {
        public int breachId { get; set; }
        public string type { get; set; }
        public int requirementId { get; set; }
        public int controlId { get; set; }
        public string? numerationToShow { get; set; }
        public string title { get; set; }
        public int breachSeverityId { get; set; }
        public int breachStatusId { get; set; }
        public int responsibleId { get; set; }
        public GetBreachsByEvaluationIdBreachSeverityDto? breachSeverity { get; set; }
        public GetBreachsByEvaluationIdBreachStatusDto? breachStatus { get; set; }
        public GetBreachsByEvaluationIdControlDto? control { get; set; }
        public GetBreachsByEvaluationIdRequirementDto? requirement { get; set; }
        public GetBreachsByEvaluationIdResponsibleDto? responsible { get; set; }
    }
    public class GetBreachsByEvaluationIdBreachSeverityDto
    {
        public string name { get; set; }
        public string color { get; set; }

    }
    public class GetBreachsByEvaluationIdBreachStatusDto
    {
        public string name { get; set; }
        public string color { get; set; }

    }
    public class GetBreachsByEvaluationIdControlDto
    {
        public int number { get; set; }
        public string name { get; set; }

    }
    public class GetBreachsByEvaluationIdRequirementDto
    {
        public int numeration { get; set; }
        public string name { get; set; }

    }
    public class GetBreachsByEvaluationIdResponsibleDto
    {
        public string name { get; set; }

    }
}
//Breach
//CreateMap<BreachEntity, GetBreachsByEvaluationIdDto>().ReverseMap();//CreateMap<BreachSeverityEntity, GetBreachsByEvaluationIdBreachSeverityDto>().ReverseMap();
//CreateMap<BreachStatusEntity, GetBreachsByEvaluationIdBreachStatusDto>().ReverseMap();
//CreateMap<ControlEntity, GetBreachsByEvaluationIdControlDto>().ReverseMap();
//CreateMap<RequirementEntity, GetBreachsByEvaluationIdRequirementDto>().ReverseMap();
//CreateMap<ResponsibleEntity, GetBreachsByEvaluationIdResponsibleDto>().ReverseMap();
