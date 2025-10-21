namespace Qualifier.Application.Database.OptionInMenu.Commands.DeleteOptionInMenu
{
    public interface IDeleteOptionInMenuCommand
    {
        Task<Object> Execute(int id, int updateUserId);
    }
}

