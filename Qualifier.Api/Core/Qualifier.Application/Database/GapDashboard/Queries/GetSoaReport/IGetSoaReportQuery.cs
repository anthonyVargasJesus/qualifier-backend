namespace Qualifier.Application.Database.GapDashboard.Queries.GetSoaReport
{
    public interface IGetSoaReportQuery
    {
        Task<Object> Execute(int companyId);
    }
}
