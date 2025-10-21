namespace Qualifier.Application.Database.DefaultRisk.Queries.GetDefaultRisksByStandardId
{
    public class GetDefaultRisksByStandardIdDto
    {
        public int defaultRiskId { get; set; }
        public int standardId { get; set; }
        public string? name { get; set; }
        public int menaceId { get; set; }
        public int vulnerabilityId { get; set; }
        public GetDefaultRisksByStandardIdMenaceDto? menace { get; set; }
        public GetDefaultRisksByStandardIdVulnerabilityDto? vulnerability { get; set; }
    }
    public class GetDefaultRisksByStandardIdMenaceDto
    {
        public string? name { get; set; }

    }
    public class GetDefaultRisksByStandardIdVulnerabilityDto
    {
        public string? name { get; set; }

    }
}

