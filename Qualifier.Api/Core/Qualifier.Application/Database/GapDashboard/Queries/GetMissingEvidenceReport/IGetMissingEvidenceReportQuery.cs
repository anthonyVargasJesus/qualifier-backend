namespace Qualifier.Application.Database.GapDashboard.Queries.GetMissingEvidenceReport
{
    public interface IGetMissingEvidenceReportQuery
    {
        Task<Object> Execute(int companyId);
    }
}
