namespace Qualifier.Application.Database.Evaluation.Commands.DeleteEvaluation
{
    public interface IDeleteEvaluationCommand
    {
        Task<Object> Execute(int id, int updateUserId);
    }
}

