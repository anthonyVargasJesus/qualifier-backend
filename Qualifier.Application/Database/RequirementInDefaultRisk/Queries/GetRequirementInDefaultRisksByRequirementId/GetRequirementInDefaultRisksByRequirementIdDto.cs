namespace Qualifier.Application.Database.RequirementInDefaultRisk.Queries.GetRequirementInDefaultRisksByRequirementId
{
    public class GetRequirementInDefaultRisksByRequirementIdDto
    {
        public int requirementInDefaultRiskId { get; set; }
        public int defaultRiskId { get; set; }
        public int requirementId { get; set; }
        public bool isActive { get; set; }
        public GetRequirementInDefaultRisksByRequirementIdDefaultRiskDto? defaultRisk { get; set; }
    }
    public class GetRequirementInDefaultRisksByRequirementIdDefaultRiskDto
    {
        public string name { get; set; }

    }
}

