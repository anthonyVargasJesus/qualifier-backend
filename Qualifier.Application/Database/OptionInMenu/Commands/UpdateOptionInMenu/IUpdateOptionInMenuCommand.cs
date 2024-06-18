namespace Qualifier.Application.Database.OptionInMenu.Commands.UpdateOptionInMenu
{
    public interface IUpdateOptionInMenuCommand
    {
        Task<Object> Execute(UpdateOptionInMenuDto model, int id);
    }
}

