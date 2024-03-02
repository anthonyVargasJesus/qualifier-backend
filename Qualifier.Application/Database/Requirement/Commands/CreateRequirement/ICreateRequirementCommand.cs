namespace Qualifier.Application.Database.Requirement.Commands.CreateRequirement
{
    public interface ICreateRequirementCommand
    {
        Task<Object> Execute(CreateRequirementDto model);
    }
}

