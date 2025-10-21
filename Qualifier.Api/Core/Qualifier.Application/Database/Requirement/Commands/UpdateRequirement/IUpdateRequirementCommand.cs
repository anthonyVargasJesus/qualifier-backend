namespace Qualifier.Application.Database.Requirement.Commands.UpdateRequirement
{
    public interface IUpdateRequirementCommand
    {
        Task<Object> Execute(UpdateRequirementDto model, int id);
    }
}

