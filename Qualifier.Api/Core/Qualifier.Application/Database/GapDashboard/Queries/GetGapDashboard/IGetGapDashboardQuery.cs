namespace Qualifier.Application.Database.GapDashboard.Queries.GetGapDashboard
{
    public interface IGetGapDashboardQuery
    {
        Task<Object> Execute(int standardId, int evaluationId, int userId, bool scopeToUser);
    }
}
