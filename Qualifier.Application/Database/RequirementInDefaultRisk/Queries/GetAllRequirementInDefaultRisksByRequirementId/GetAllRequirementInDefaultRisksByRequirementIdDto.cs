namespace Qualifier.Application.Database.RequirementInDefaultRisk.Queries.GetAllRequirementInDefaultRisksByRequirementId
{
    public class GetAllRequirementInDefaultRisksByRequirementIdDto
    {
        public int requirementInDefaultRiskId { get; set; }
        public int defaultRiskId { get; set; }
        public int requirementId { get; set; }
        public bool isActive { get; set; }
        public GetAllRequirementInDefaultRisksByRequirementIdDefaultRiskDto? defaultRisk { get; set; }
    }
    public class GetAllRequirementInDefaultRisksByRequirementIdDefaultRiskDto
    {
        public string name { get; set; }

    }
}

