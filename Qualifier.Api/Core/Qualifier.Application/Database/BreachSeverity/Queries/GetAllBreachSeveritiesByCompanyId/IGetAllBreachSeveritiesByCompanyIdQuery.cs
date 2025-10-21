namespace Qualifier.Application.Database.BreachSeverity.Queries.GetAllBreachSeveritiesByCompanyId
{
    public interface IGetAllBreachSeveritiesByCompanyIdQuery
    {
        Task<Object> Execute(int companyId);
    }
}

