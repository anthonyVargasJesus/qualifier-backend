namespace Qualifier.Application.Database.Risk.Queries.GetRisksByCompanyId
{
    public interface IGetRisksByCompanyIdQuery
    {
        Task<Object> Execute(int skip, int pageSize, string search, int companyId);
    }
}

