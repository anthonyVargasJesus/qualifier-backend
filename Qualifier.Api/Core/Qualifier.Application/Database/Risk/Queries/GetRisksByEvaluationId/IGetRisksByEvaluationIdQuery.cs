namespace Qualifier.Application.Database.Risk.Queries.GetRisksByEvaluationId
{
    public interface IGetRisksByEvaluationIdQuery
    {
        Task<Object> Execute(int skip, int pageSize, string search, int riskStatusId);
    }
}

