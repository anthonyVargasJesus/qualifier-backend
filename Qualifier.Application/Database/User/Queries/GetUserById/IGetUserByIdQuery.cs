namespace Qualifier.Application.Database.User.Queries.GetUserById
{
    public interface IGetUserByIdQuery
    {
        Task<Object> Execute(int userId);
    }
}

