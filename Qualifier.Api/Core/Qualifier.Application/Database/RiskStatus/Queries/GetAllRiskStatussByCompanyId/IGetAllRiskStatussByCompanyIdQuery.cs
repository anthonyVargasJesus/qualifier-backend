namespace Qualifier.Application.Database.RiskStatus.Queries.GetAllRiskStatussByCompanyId
{
    public interface IGetAllRiskStatussByCompanyIdQuery
    {
        Task<Object> Execute(int companyId);
    }
}

