namespace Qualifier.Application.Database.ActionPlan.Queries.GetActionPlanCalendar
{
    public interface IGetActionPlanCalendarQuery
    {
        Task<Object> Execute(int companyId, int evaluationId, int year, int skip, int pageSize);
    }
}
