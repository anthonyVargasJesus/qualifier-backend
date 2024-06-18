namespace Qualifier.Application.Database.Menu.Commands.CreateMenu
{
    public interface ICreateMenuCommand
    {
        Task<Object> Execute(CreateMenuDto model);
    }
}

