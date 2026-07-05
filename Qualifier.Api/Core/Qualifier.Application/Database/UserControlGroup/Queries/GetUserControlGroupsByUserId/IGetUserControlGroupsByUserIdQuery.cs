namespace Qualifier.Application.Database.UserControlGroup.Queries.GetUserControlGroupsByUserId
{
    public interface IGetUserControlGroupsByUserIdQuery
    {
        Task<Object> Execute(int userId, int standardId);
    }
}
