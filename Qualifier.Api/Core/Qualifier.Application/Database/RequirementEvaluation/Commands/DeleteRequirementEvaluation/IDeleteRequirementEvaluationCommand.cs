namespace Qualifier.Application.Database.RequirementEvaluation.Commands.DeleteRequirementEvaluation
{
    public interface IDeleteRequirementEvaluationCommand
    {
        Task<Object> Execute(int id, int updateUserId);
    }
}

