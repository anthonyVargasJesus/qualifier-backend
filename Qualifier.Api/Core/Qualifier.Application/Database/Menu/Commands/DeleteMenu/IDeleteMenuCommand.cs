namespace Qualifier.Application.Database.Menu.Commands.DeleteMenu
{
    public interface IDeleteMenuCommand
    {
        Task<Object> Execute(int id, int updateUserId);
    }
}

