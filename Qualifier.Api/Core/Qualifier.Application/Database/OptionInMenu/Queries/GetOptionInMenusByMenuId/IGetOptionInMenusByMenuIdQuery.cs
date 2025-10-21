namespace Qualifier.Application.Database.OptionInMenu.Queries.GetOptionInMenusByMenuId
{
    public interface IGetOptionInMenusByMenuIdQuery
    {
        Task<Object> Execute(int skip, int pageSize, string search, int menuId);
    }
}

