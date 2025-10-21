namespace Qualifier.Application.Database.RequirementInDefaultRisk.Queries.GetRequirementInDefaultRisksByRequirementId
{
    public interface IGetRequirementInDefaultRisksByRequirementIdQuery
    {
        Task<Object> Execute(int skip, int pageSize, string search, int requirementId);
    }
}

