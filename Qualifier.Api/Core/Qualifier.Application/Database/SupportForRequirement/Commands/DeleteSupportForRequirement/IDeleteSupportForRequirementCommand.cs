namespace Qualifier.Application.Database.SupportForRequirement.Commands.DeleteSupportForRequirement
{
    public interface IDeleteSupportForRequirementCommand
    {
        Task<Object> Execute(int id, int updateUserId);
    }
}

