namespace Qualifier.Application.Database.ActionPlan.Queries.GetActionPlanCountsByBreach
{
    public interface IGetActionPlanCountsByBreachQuery
    {
        Task<Object> Execute(int companyId, int evaluationId);
    }
}
