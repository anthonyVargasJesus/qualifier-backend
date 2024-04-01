namespace Qualifier.Application.Database.ControlEvaluation.Commands.UpdateControlEvaluation
{
    public interface IUpdateControlEvaluationCommand
    {
        Task<Object> Execute(UpdateControlEvaluationDto model, int id);
    }
}

