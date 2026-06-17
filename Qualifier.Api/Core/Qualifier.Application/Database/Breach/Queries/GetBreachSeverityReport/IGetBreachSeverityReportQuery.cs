namespace Qualifier.Application.Database.Breach.Queries.GetBreachSeverityReport
{
    public interface IGetBreachSeverityReportQuery
    {
        Task<Object> Execute(int companyId);
    }
}
