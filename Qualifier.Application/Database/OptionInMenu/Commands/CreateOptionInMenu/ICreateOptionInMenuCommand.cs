namespace Qualifier.Application.Database.OptionInMenu.Commands.CreateOptionInMenu
{
    public interface ICreateOptionInMenuCommand
    {
        Task<Object> Execute(CreateOptionInMenuDto model);
    }
}

