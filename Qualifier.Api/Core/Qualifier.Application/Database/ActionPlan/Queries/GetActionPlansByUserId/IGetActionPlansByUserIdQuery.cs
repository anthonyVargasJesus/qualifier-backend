namespace Qualifier.Application.Database.ActionPlan.Queries.GetActionPlansByUserId
{
    public interface IGetActionPlansByUserIdQuery
    {
        Task<Object> Execute(int userId, int evaluationId);
    }
}
