namespace Qualifier.Application.Database.Menu.Queries.GetMenuById
{
    public interface IGetMenuByIdQuery
    {
        Task<Object> Execute(int menuId);
    }
}

