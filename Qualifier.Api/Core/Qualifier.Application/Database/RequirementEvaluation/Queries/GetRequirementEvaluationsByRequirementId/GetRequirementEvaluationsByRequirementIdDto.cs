namespace Qualifier.Application.Database.RequirementEvaluation.Queries.GetRequirementEvaluationsByRequirementId
{
    public class GetRequirementEvaluationsByRequirementIdDto
    {
        public long requirementEvaluationId { get; set; }
        public int maturityLevelId { get; set; }
        public decimal value { get; set; }
        public int responsibleId { get; set; }
        public string justification { get; set; }
        public string improvementActions { get; set; }
        //public GetRequirementEvaluationsByRequirementIdEvaluationDto? evaluation { get; set; }
        public GetRequirementEvaluationsByRequirementIdMaturityLevelDto? maturityLevel { get; set; }
        public GetRequirementEvaluationsByRequirementIdResponsibleDto? responsible { get; set; }
    }
    //public class GetRequirementEvaluationsByRequirementIdEvaluationDto
    //{
    //    public string name { get; set; }

    //}
    public class GetRequirementEvaluationsByRequirementIdMaturityLevelDto
    {
        public string abbreviation { get; set; }
        public string color { get; set; }

    }
    public class GetRequirementEvaluationsByRequirementIdResponsibleDto
    {
        public string name { get; set; }

    }
}
