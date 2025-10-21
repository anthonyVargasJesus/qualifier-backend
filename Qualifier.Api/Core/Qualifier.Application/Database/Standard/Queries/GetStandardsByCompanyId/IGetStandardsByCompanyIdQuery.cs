namespace Qualifier.Application.Database.Standard.Queries.GetStandardsByCompanyId
{
    public interface IGetStandardsByCompanyIdQuery
    {
        Task<Object> Execute(int skip, int pageSize, string search, int companyId);
    }
}

