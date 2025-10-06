namespace Qualifier.Application.Database.ActionPlanPriority.Queries.GetAllActionPlanPrioritiesByCompanyId
{
    public interface IGetAllActionPlanPrioritiesByCompanyIdQuery
    {
        Task<Object> Execute(int companyId);
    }
}

