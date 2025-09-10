namespace Qualifier.Application.Database.RequirementInDefaultRisk.Queries.GetAllRequirementInDefaultRisksByRequirementId
{
    public interface IGetAllRequirementInDefaultRisksByRequirementIdQuery
    {
        Task<Object> Execute(int requirementId);
    }
}

