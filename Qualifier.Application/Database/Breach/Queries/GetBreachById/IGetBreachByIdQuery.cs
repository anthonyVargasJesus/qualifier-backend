namespace Qualifier.Application.Database.Breach.Queries.GetBreachById
{
    public interface IGetBreachByIdQuery
    {
        Task<Object> Execute(int breachId);
    }
}

