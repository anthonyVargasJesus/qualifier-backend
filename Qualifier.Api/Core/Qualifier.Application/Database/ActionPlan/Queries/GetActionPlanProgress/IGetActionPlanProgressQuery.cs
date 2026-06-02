namespace Qualifier.Application.Database.ActionPlan.Queries.GetActionPlanProgress
{
    public interface IGetActionPlanProgressQuery
    {
        Task<Object> Execute(int companyId);
    }
}
