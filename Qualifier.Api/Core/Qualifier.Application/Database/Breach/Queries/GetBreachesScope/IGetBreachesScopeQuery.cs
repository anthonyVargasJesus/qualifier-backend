namespace Qualifier.Application.Database.Breach.Queries.GetBreachesScope
{
    public interface IGetBreachesScopeQuery
    {
        Task<Object> Execute(int evaluationId);
    }
}
