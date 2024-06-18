namespace Qualifier.Application.Database.MenuInRole.Queries.GetMenuInRolesByRoleId
{
    public interface IGetMenuInRolesByRoleIdQuery
    {
        Task<Object> Execute(int skip, int pageSize, string search, int roleId);
    }
}

