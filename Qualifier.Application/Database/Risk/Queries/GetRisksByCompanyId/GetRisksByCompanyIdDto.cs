namespace Qualifier.Application.Database.Risk.Queries.GetRisksByCompanyId
{
    public class GetRisksByCompanyIdDto
    {
        public int riskId { get; set; }
        public int activesInventoryId { get; set; }
        public string activesInventoryNumber { get; set; }
        public string macroProcess { get; set; }
        public string subProcess { get; set; }
        public string activesInventoryName { get; set; }
        public decimal activesInventoryValuation { get; set; }
        public int menaceId { get; set; }
        public int vulnerabilityId { get; set; }
        public decimal menaceLevel { get; set; }
        public decimal vulnerabilityLevel { get; set; }
        public int controlId { get; set; }
        public decimal riskAssessmentValue { get; set; }
        public int riskLevelId { get; set; }
        public string treatmentMethod { get; set; }
        public int controlTypeId { get; set; }
        public GetRisksByCompanyIdControlDto? control { get; set; }
        public GetRisksByCompanyIdControlTypeDto? controlType { get; set; }
        public GetRisksByCompanyIdMenaceDto? menace { get; set; }
        public GetRisksByCompanyIdResponsibleDto? responsible { get; set; }
        public GetRisksByCompanyIdRiskLevelDto? riskLevel { get; set; }
        public GetRisksByCompanyIdVulnerabilityDto? vulnerability { get; set; }
    }
    public class GetRisksByCompanyIdControlDto
    {
        public string name { get; set; }

    }
    public class GetRisksByCompanyIdControlTypeDto
    {
        public string name { get; set; }

    }
    public class GetRisksByCompanyIdMenaceDto
    {
        public string name { get; set; }

    }
    public class GetRisksByCompanyIdResponsibleDto
    {
        public string name { get; set; }

    }
    public class GetRisksByCompanyIdRiskLevelDto
    {
        public string name { get; set; }

    }
    public class GetRisksByCompanyIdVulnerabilityDto
    {
        public string name { get; set; }

    }
}


