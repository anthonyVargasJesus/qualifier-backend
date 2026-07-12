namespace Qualifier.Application.Database.ActionPlan.Queries.GetActionPlanCountsByUser
{
    public interface IGetActionPlanCountsByUserQuery
    {
        Task<Object> Execute(int companyId);
    }
}
