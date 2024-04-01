namespace Qualifier.Application.Database.ControlEvaluation.Commands.DeleteControlEvaluation
{
    public interface IDeleteControlEvaluationCommand
    {
        Task<Object> Execute(int id, int updateUserId);
    }
}

