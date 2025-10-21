namespace Qualifier.Application.Database.Risk.Queries.GetAllRisksByEvaluationId
{
    public interface IGetAllRisksByEvaluationIdQuery
    {
        Task<Object> Execute(int evaluationId);
    }
}

