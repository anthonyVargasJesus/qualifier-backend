namespace Qualifier.Application.Database.Policy.Queries.GetAllPoliciesByStandardId
{
    public interface IGetAllPoliciesByStandardIdQuery
    {
        Task<Object> Execute(int standardId);
    }
}

