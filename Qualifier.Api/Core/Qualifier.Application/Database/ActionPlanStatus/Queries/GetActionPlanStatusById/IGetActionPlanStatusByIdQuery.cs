namespace Qualifier.Application.Database.ActionPlanStatus.Queries.GetActionPlanStatusById
{
    public interface IGetActionPlanStatusByIdQuery
    {
        Task<Object> Execute(int actionPlanStatusId);
    }
}

