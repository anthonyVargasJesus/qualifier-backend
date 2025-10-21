namespace Qualifier.Application.Database.RiskLevel.Queries.GetAllRiskLevelsByCompanyId
{
    public interface IGetAllRiskLevelsByCompanyIdQuery
    {
        Task<Object> Execute(int companyId);
    }
}

