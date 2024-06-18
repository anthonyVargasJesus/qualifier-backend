namespace Qualifier.Application.Database.RoleInUser.Commands.DeleteRoleInUser
{
    public interface IDeleteRoleInUserCommand
    {
        Task<Object> Execute(int id, int updateUserId);
    }
}

