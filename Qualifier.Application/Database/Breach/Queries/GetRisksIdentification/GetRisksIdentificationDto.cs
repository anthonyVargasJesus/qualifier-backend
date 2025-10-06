
namespace Qualifier.Application.Database.Breach.Queries.GetRisksIdentification
{
    public class GetRisksIdentificationResponseDto
    {
        public GetRisksIdentificationCurrentEvaluationDto? currentEvaluation { get; set; }
        public List<GetRisksIdentificationDto>? breachs { get; set; }
        public Object? pagination { get; set; }
    }
    public class GetRisksIdentificationCurrentEvaluationDto
    {
        public int evaluationId { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public string? description { get; set; }
    }

    public class GetRisksIdentificationDto
    {
        public int breachId { get; set; }
        public string? type { get; set; }
        public int requirementId { get; set; }
        public int controlId { get; set; }
        public string? numerationToShow { get; set; }
        public string? title { get; set; }
        public int breachSeverityId { get; set; }
        public int breachStatusId { get; set; }
        public int responsibleId { get; set; }
        public GetRisksIdentificationBreachSeverityDto? breachSeverity { get; set; }
        public GetRisksIdentificationControlDto? control { get; set; }
        public GetRisksIdentificationRequirementDto? requirement { get; set; }
        public List<GetRisksIdentificationDefaultRiskDto>? defaultRisks { get; set; }
    }
    public class GetRisksIdentificationBreachSeverityDto
    {
        public string? name { get; set; }
        public string? color { get; set; }

    }
    public class GetRisksIdentificationControlDto
    {
        public int number { get; set; }
        public string? name { get; set; }

    }
    public class GetRisksIdentificationRequirementDto
    {
        public int numeration { get; set; }
        public string? name { get; set; }

    }

    public class GetRisksIdentificationDefaultRiskDto
    {
        public int defaultRiskId { get; set; }
        public string? name { get; set; }
    }

}
