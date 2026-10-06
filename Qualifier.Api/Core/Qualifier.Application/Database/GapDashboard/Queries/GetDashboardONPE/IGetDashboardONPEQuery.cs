namespace Qualifier.Application.Database.GapDashboard.Queries.GetDashboardONPE
{
    public interface IGetDashboardONPEQuery
    {
        Task<Object> Execute(int companyId);
    }
}
