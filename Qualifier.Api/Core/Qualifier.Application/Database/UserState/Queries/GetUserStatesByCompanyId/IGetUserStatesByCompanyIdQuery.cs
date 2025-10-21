namespace Qualifier.Application.Database.UserState.Queries.GetUserStatesByCompanyId
{
    public interface IGetUserStatesByCompanyIdQuery
    {
        Task<Object> Execute(int skip, int pageSize, string search, int companyId);
    }
}

