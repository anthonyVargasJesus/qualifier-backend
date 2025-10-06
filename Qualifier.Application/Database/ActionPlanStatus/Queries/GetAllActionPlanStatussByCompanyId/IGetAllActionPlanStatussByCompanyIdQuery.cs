namespace Qualifier.Application.Database.ActionPlanStatus.Queries.GetAllActionPlanStatussByCompanyId
{
    public interface IGetAllActionPlanStatussByCompanyIdQuery
    {
        Task<Object> Execute(int companyId);
    }
}

