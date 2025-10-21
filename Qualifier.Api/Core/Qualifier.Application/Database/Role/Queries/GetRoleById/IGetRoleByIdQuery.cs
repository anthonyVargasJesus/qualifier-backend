namespace Qualifier.Application.Database.Role.Queries.GetRoleById
{
    public interface IGetRoleByIdQuery
    {
        Task<Object> Execute(int roleId);
    }
}

