namespace Qualifier.Application.Database.Breach.Queries.GetBreachAgingReport
{
    public interface IGetBreachAgingReportQuery
    {
        Task<Object> Execute(int companyId);
    }
}
