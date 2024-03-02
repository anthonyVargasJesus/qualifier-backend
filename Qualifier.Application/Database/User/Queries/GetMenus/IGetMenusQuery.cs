namespace Qualifier.Application.Database.User.Queries.GetMenus
{
    public interface IGetMenusQuery
    {
        Task<Object> Execute(int userId);
    }
}
