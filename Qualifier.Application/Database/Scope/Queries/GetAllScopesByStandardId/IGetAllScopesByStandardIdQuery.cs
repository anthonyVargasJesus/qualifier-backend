namespace Qualifier.Application.Database.Scope.Queries.GetAllScopesByStandardId
{
    public interface IGetAllScopesByStandardIdQuery
    {
        Task<Object> Execute(int standardId);
    }
}

