namespace Qualifier.Application.Database.ActiveType.Queries.GetActiveTypesByCompanyId
{
    public interface IGetActiveTypesByCompanyIdQuery
    {
        Task<Object> Execute(int skip, int pageSize, string search, int companyId);
    }
}

