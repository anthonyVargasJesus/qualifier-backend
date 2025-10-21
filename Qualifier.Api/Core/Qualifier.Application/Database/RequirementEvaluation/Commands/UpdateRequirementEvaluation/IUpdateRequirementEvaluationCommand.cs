namespace Qualifier.Application.Database.RequirementEvaluation.Commands.UpdateRequirementEvaluation
{
    public interface IUpdateRequirementEvaluationCommand
    {
        Task<Object> Execute(UpdateRequirementEvaluationDto model, int id);
    }
}

