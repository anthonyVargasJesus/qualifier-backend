namespace Qualifier.Application.Database.RequirementInDefaultRisk.Queries.GetRequirementInDefaultRiskById
{
    public interface IGetRequirementInDefaultRiskByIdQuery
    {
        Task<Object> Execute(int requirementInDefaultRiskId);
    }
}

