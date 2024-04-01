namespace Qualifier.Application.Database.RequirementEvaluation.Commands.CreateRequirementEvaluation
{
    public interface ICreateRequirementEvaluationCommand
    {
        Task<Object> Execute(CreateRequirementEvaluationDto model);
    }
}

