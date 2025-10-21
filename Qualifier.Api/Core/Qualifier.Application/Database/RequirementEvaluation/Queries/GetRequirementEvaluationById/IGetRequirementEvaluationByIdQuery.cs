namespace Qualifier.Application.Database.RequirementEvaluation.Queries.GetRequirementEvaluationById
{
    public interface IGetRequirementEvaluationByIdQuery
    {
        Task<Object> Execute(int requirementEvaluationId);
    }
}

