namespace Qualifier.Application.Database.User.Queries.GetUsersByCompanyId
{
    public interface IGetUsersByCompanyIdQuery
    {
        Task<Object> Execute(int skip, int pageSize, string search, int companyId);
    }
}

