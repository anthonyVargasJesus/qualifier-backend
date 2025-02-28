namespace Qualifier.Application.Database.Scope.Queries.GetScopesByStandardId
{
    public interface IGetScopesByStandardIdQuery
    {
        Task<Object> Execute(int skip, int pageSize, string search, int standardId);
    }
}

