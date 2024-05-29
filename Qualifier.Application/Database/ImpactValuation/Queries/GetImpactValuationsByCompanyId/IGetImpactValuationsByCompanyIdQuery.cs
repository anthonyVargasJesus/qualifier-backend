namespace Qualifier.Application.Database.ImpactValuation.Queries.GetImpactValuationsByCompanyId
{
    public interface IGetImpactValuationsByCompanyIdQuery
    {
        Task<Object> Execute(int skip, int pageSize, string search, int companyId);
    }
}

