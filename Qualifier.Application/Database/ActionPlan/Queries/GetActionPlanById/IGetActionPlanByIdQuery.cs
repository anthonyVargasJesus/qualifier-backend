namespace Qualifier.Application.Database.ActionPlan.Queries.GetActionPlanById
{
    public interface IGetActionPlanByIdQuery
    {
        Task<Object> Execute(int actionPlanId);
    }
}

