namespace Qualifier.Application.Database.RoleInUser.Commands.UpdateRoleInUser
{
    public interface IUpdateRoleInUserCommand
    {
        Task<Object> Execute(UpdateRoleInUserDto model, int id);
    }
}

