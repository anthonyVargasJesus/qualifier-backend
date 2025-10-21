namespace Qualifier.Application.Database.Policy.Queries.GetPoliciesByStandardId
{
    public interface IGetPoliciesByStandardIdQuery
    {
        Task<Object> Execute(int skip, int pageSize, string search, int standardId);
    }
}

