namespace Qualifier.Application.Database.Evaluation.Commands.UpdateEvaluation
{
    public interface IUpdateEvaluationCommand
    {
        Task<Object> Execute(UpdateEvaluationDto model, int id);
    }
}

