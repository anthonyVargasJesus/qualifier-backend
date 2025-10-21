namespace Qualifier.Application.Database.RoleInUser.Commands.CreateRoleInUser
{
    public interface ICreateRoleInUserCommand
    {
        Task<Object> Execute(CreateRoleInUserDto model);
    }
}

