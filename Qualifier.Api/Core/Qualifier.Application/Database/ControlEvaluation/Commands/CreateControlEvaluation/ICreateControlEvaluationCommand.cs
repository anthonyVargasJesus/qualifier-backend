namespace Qualifier.Application.Database.ControlEvaluation.Commands.CreateControlEvaluation
{
    public interface ICreateControlEvaluationCommand
    {
        Task<Object> Execute(CreateControlEvaluationDto model);
    }
}

