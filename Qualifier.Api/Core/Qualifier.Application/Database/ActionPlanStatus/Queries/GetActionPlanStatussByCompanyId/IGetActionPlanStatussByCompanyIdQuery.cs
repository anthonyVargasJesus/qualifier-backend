namespace Qualifier.Application.Database.ActionPlanStatus.Queries.GetActionPlanStatussByCompanyId
{
    public interface IGetActionPlanStatussByCompanyIdQuery
    {
        Task<Object> Execute(int skip, int pageSize, string search, int companyId);
    }
}

