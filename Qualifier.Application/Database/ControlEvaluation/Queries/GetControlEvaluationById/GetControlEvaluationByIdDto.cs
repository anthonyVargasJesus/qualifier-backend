namespace Qualifier.Application.Database.ControlEvaluation.Queries.GetControlEvaluationById
{
    public class GetControlEvaluationByIdDto
    {
        public long controlEvaluationId { get; set; }
        public int evaluationId { get; set; }
        public int controlId { get; set; }
        public int maturityLevelId { get; set; }
        public decimal value { get; set; }
        public int responsibleId { get; set; }
        public string justification { get; set; }
        public string improvementActions { get; set; }
        public string controlDescription { get; set; }
        public string controlType { get; set; }
        public int standardId { get; set; }
        public List<int> arrayReferenceDocumentations { get; set; }
    }
}

