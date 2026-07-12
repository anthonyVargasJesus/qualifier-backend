namespace Qualifier.Application.Database.ActionPlan.Queries.GetOverdueActionPlansReport
{
    public interface IGetOverdueActionPlansReportQuery
    {
        Task<Object> Execute(int companyId);
    }
}
