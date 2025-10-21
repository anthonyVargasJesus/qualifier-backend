namespace Qualifier.Application.Database.RiskStatus.Queries.GetRiskStatusById
{
    public interface IGetRiskStatusByIdQuery
    {
        Task<Object> Execute(int riskStatusId);
    }
}

