namespace Qualifier.Application.Database.Evaluation.Queries.GetPendingDocumentation
{
    public class GetPendingDocumentationDto
    {
        public int documentationId { get; set; }
        public string name { get; set; }
        public List<GetPendingDocumentationRequirementDto> requirements { get; set; }
        public List<GetPendingDocumentationControlDto> controls { get; set; }
    }

    public class GetPendingDocumentationRequirementDto
    {
        public int requirementId { get; set; }
        public string name { get; set; }
        public string numerationToShow { get; set; }
    }

    public class GetPendingDocumentationControlDto
    {
        public int controlId { get; set; }
        public int number { get; set; }
        public string name { get; set; }
        public string numerationToShow { get; set; }
    }
}
