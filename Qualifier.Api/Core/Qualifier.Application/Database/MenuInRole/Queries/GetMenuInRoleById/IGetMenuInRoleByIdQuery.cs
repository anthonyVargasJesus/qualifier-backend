namespace Qualifier.Application.Database.MenuInRole.Queries.GetMenuInRoleById
{
    public interface IGetMenuInRoleByIdQuery
    {
        Task<Object> Execute(int menuInRoleId);
    }
}

