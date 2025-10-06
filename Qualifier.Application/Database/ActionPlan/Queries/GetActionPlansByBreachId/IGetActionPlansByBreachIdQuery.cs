namespace Qualifier.Application.Database.ActionPlan.Queries.GetActionPlansByBreachId
{
    public interface IGetActionPlansByBreachIdQuery
    {
        Task<Object> Execute(int skip, int pageSize, string search, int breachId);
    }
}

