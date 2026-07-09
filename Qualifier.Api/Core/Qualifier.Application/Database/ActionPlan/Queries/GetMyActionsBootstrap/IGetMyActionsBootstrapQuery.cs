namespace Qualifier.Application.Database.ActionPlan.Queries.GetMyActionsBootstrap
{
    public interface IGetMyActionsBootstrapQuery
    {
        Task<Object> Execute(int companyId, int userId);
    }
}
