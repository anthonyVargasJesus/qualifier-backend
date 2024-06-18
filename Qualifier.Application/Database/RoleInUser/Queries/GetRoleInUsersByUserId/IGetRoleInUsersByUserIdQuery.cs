namespace Qualifier.Application.Database.RoleInUser.Queries.GetRoleInUsersByUserId
{
    public interface IGetRoleInUsersByUserIdQuery
    {
        Task<Object> Execute(int skip, int pageSize, string search, int userId);
    }
}

