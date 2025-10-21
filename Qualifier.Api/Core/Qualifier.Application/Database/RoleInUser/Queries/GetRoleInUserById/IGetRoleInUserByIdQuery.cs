namespace Qualifier.Application.Database.RoleInUser.Queries.GetRoleInUserById
{
    public interface IGetRoleInUserByIdQuery
    {
        Task<Object> Execute(int roleInUserId);
    }
}

