namespace Qualifier.Application.Database.Breach.Queries.GetBreachesWithoutActionPlan
{
    public interface IGetBreachesWithoutActionPlanQuery
    {
        Task<Object> Execute(int companyId);
    }
}
