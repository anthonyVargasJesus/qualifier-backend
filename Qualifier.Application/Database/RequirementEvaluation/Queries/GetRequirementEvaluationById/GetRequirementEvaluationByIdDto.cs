namespace Qualifier.Application.Database.RequirementEvaluation.Queries.GetRequirementEvaluationById
{
    public class GetRequirementEvaluationByIdDto
    {
        public long requirementEvaluationId { get; set; }
        public int evaluationId { get; set; }
        public int requirementId { get; set; }
        public int maturityLevelId { get; set; }
        public decimal value { get; set; }
        public int responsibleId { get; set; }
        public string justification { get; set; }
        public string improvementActions { get; set; }
        public int standardId { get; set; }
        public int companyId { get; set; }
        public List<int> arrayReferenceDocumentations { get; set; }
    }
}

