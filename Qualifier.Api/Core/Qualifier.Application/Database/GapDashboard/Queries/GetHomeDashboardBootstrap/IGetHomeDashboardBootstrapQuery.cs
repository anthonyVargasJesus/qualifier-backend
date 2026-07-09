namespace Qualifier.Application.Database.GapDashboard.Queries.GetHomeDashboardBootstrap
{
    public interface IGetHomeDashboardBootstrapQuery
    {
        Task<Object> Execute(int userId, int notificationsPageSize = 3);
    }
}
