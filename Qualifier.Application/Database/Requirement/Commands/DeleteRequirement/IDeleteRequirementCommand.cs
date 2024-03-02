namespace Qualifier.Application.Database.Requirement.Commands.DeleteRequirement
{
    public interface IDeleteRequirementCommand
    {
        Task<Object> Execute(int id, int updateUserId);
    }
}

