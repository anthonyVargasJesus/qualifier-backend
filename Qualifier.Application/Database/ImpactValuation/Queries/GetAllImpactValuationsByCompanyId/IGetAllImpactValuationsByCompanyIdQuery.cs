namespace Qualifier.Application.Database.ImpactValuation.Queries.GetAllImpactValuationsByCompanyId
{
    public interface IGetAllImpactValuationsByCompanyIdQuery
    {
        Task<Object> Execute(int companyId);
    }
}

