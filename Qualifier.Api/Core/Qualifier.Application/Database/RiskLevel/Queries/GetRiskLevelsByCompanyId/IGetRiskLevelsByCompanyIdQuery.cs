namespace Qualifier.Application.Database.RiskLevel.Queries.GetRiskLevelsByCompanyId
{
    public interface IGetRiskLevelsByCompanyIdQuery
    {
        Task<Object> Execute(int skip, int pageSize, string search, int companyId);
    }
}

