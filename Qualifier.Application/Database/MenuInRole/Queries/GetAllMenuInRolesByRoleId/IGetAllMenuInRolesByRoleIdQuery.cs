namespace Qualifier.Application.Database.MenuInRole.Queries.GetAllMenuInRolesByRoleId
{
    public interface IGetAllMenuInRolesByRoleIdQuery
    {
        Task<Object> Execute(int roleId);
    }
}

