namespace Qualifier.Application.Database.DefaultRisk.Queries.GetAllDefaultRisksByStandardId
{
    public class GetAllDefaultRisksByStandardIdDto
    {
        public int defaultRiskId { get; set; }
        public int standardId { get; set; }
        public string name { get; set; }
        public int menaceId { get; set; }
        public int vulnerabilityId { get; set; }
        public GetAllDefaultRisksByStandardIdMenaceDto? menace { get; set; }
        public GetAllDefaultRisksByStandardIdVulnerabilityDto? vulnerability { get; set; }
    }
    public class GetAllDefaultRisksByStandardIdMenaceDto
    {
        public string name { get; set; }

    }
    public class GetAllDefaultRisksByStandardIdVulnerabilityDto
    {
        public string name { get; set; }

    }
}

