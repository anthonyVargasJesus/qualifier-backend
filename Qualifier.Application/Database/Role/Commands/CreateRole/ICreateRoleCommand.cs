namespace Qualifier.Application.Database.Role.Commands.CreateRole
{
    public interface ICreateRoleCommand
    {
        Task<Object> Execute(CreateRoleDto model);
    }
}

