namespace Qualifier.Application.Database.BreachStatus.Queries.GetAllBreachStatussByCompanyId
{
    public interface IGetAllBreachStatussByCompanyIdQuery
    {
        Task<Object> Execute(int companyId);
    }
}

