namespace Qualifier.Application.Database.ActionPlanPriority.Queries.GetActionPlanPrioritiesByCompanyId
{
    public interface IGetActionPlanPrioritiesByCompanyIdQuery
    {
        Task<Object> Execute(int skip, int pageSize, string search, int companyId);
    }
}

