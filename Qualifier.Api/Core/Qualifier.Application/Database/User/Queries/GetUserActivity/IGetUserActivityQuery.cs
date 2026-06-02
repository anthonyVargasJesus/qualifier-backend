namespace Qualifier.Application.Database.User.Queries.GetUserActivity
{
    public interface IGetUserActivityQuery
    {
        Task<Object> Execute(int companyId);
    }
}
