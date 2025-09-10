namespace Qualifier.Application.Database.RequirementInDefaultRisk.Queries.GetRequirementInDefaultRiskById
{
    public class GetRequirementInDefaultRiskByIdDto
    {
        public int requirementInDefaultRiskId { get; set; }
        public int defaultRiskId { get; set; }
        public int requirementId { get; set; }
        public bool? isActive { get; set; }

    }
}

