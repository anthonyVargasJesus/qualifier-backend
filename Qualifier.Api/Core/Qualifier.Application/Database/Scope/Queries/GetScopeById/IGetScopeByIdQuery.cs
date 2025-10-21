namespace Qualifier.Application.Database.Scope.Queries.GetScopeById
{
    public interface IGetScopeByIdQuery
    {
        Task<Object> Execute(int scopeId);
    }
}

