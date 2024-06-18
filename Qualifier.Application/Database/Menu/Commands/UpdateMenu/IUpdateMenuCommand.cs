namespace Qualifier.Application.Database.Menu.Commands.UpdateMenu
{
    public interface IUpdateMenuCommand
    {
        Task<Object> Execute(UpdateMenuDto model, int id);
    }
}

