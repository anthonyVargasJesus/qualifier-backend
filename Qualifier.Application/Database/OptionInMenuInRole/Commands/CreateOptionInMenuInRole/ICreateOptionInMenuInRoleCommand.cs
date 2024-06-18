namespace Qualifier.Application.Database.OptionInMenuInRole.Commands.CreateOptionInMenuInRole
{
    public interface ICreateOptionInMenuInRoleCommand
    {
        Task<Object> Execute(CreateOptionInMenuInRoleDto model);
    }
}

