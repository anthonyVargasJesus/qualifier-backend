namespace Qualifier.Application.Database.Role.Commands.UpdateRole
{
    public interface IUpdateRoleCommand
    {
        Task<Object> Execute(UpdateRoleDto model, int id);
    }
}

