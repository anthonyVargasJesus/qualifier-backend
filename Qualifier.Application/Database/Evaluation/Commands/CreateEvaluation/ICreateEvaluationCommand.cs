namespace Qualifier.Application.Database.Evaluation.Commands.CreateEvaluation
{
    public interface ICreateEvaluationCommand
    {
        Task<Object> Execute(CreateEvaluationDto model);
    }
}

