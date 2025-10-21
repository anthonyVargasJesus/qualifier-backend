namespace Qualifier.Application.Database.Breach.Queries.GetBreachsByEvaluationId
{
    public interface IGetBreachsByEvaluationIdQuery
    {
        Task<Object> Execute(int skip, int pageSize, string search, int evaluationId, int companyId);
    }
}

