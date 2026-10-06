namespace Qualifier.Application.Database.Gap.Queries.GetPlanDeAccionBootstrap
{
    public interface IGetPlanDeAccionBootstrapQuery
    {
        Task<Object> Execute(int companyId, int userId, bool scopeToUser = true);
    }
}
