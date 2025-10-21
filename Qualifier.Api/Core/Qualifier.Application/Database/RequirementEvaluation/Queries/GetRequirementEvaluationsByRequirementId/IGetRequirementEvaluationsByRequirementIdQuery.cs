namespace Qualifier.Application.Database.RequirementEvaluation.Queries.GetRequirementEvaluationsByRequirementId
{
    public interface IGetRequirementEvaluationsByRequirementIdQuery
    {
        Task<Object> Execute(int skip, int pageSize, int requirementId);
    }
}

