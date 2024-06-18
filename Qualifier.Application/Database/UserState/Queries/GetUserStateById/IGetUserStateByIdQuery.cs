namespace Qualifier.Application.Database.UserState.Queries.GetUserStateById
{
    public interface IGetUserStateByIdQuery
    {
        Task<Object> Execute(int userStateId);
    }
}

