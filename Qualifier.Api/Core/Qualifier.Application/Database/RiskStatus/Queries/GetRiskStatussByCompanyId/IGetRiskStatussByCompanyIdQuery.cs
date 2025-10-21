namespace Qualifier.Application.Database.RiskStatus.Queries.GetRiskStatussByCompanyId
{
    public interface IGetRiskStatussByCompanyIdQuery
    {
        Task<Object> Execute(int skip, int pageSize, string search, int companyId);
    }
}

