namespace Qualifier.Application.Database.RoleInUser.Queries.GetAllRoleInUsersByUserId
{
    public interface IGetAllRoleInUsersByUserIdQuery
    {
        Task<Object> Execute(int userId);
    }
}

