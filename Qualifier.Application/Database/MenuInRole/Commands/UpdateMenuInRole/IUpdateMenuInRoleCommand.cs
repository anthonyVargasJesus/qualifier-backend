namespace Qualifier.Application.Database.MenuInRole.Commands.UpdateMenuInRole
{
    public interface IUpdateMenuInRoleCommand
    {
        Task<Object> Execute(UpdateMenuInRoleDto model, int id);
    }
}

