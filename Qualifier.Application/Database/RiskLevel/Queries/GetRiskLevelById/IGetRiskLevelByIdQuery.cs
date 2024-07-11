namespace Qualifier.Application.Database.RiskLevel.Queries.GetRiskLevelById
{
    public interface IGetRiskLevelByIdQuery
    {
        Task<Object> Execute(int riskLevelId);
    }
}

