namespace Qualifier.Application.Database.RequirementEvaluation.Queries.GetRequirementEvaluationByProcess
{
    public class GetRequirementEvaluationsByProcessDto
    {
        public long requirementEvaluationId { get; set; }
        public int maturityLevelId { get; set; }
        public decimal value { get; set; }
        public int responsibleId { get; set; }
        public string justification { get; set; }
        public string improvementActions { get; set; }
        public GetRequirementEvaluationsByProcessMaturityLevelDto? maturityLevel { get; set; }
        public GetRequirementEvaluationsByProcessResponsibleDto? responsible { get; set; }

        public GetRequirementEvaluationsByProcessChildRequirementDto requirement { get; set; }
    }

    public class GetRequirementEvaluationsByProcessRequirementDto
    {
        public int requirementId { get; set; }
        public int numeration { get; set; }
        public string name { get; set; }
        public int level { get; set; }
        public int parentId { get; set; }
        public string numerationToShow { get; set; }
        public List<GetRequirementEvaluationsByProcessDto> requirementEvaluations { get; set; }
        public List<GetRequirementEvaluationsByProcessRequirementDto> children { get; set; }
    }

    public class GetRequirementEvaluationsByProcessChildRequirementDto
    {
        public int requirementId { get; set; }
        public string name { get; set; }
        public string numerationToShow { get; set; }
    }

    public class GetRequirementEvaluationsByProcessMaturityLevelDto
    {
        public string abbreviation { get; set; }
        public string color { get; set; }

    }
    public class GetRequirementEvaluationsByProcessResponsibleDto
    {
        public string name { get; set; }

    }

}
