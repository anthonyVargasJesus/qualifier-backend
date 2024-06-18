namespace Qualifier.Application.Database.Role.Commands.DeleteRole
{
    public interface IDeleteRoleCommand
    {
        Task<Object> Execute(int id, int updateUserId);
    }
}

