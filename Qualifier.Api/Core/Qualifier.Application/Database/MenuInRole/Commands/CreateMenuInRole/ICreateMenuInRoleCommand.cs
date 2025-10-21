namespace Qualifier.Application.Database.MenuInRole.Commands.CreateMenuInRole
{
    public interface ICreateMenuInRoleCommand
    {
        Task<Object> Execute(CreateMenuInRoleDto model);
    }
}

