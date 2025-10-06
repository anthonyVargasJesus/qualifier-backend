namespace Qualifier.Application.Database.ActionPlanPriority.Queries.GetActionPlanPriorityById
{
    public interface IGetActionPlanPriorityByIdQuery
    {
        Task<Object> Execute(int actionPlanPriorityId);
    }
}

