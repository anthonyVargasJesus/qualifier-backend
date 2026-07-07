namespace Qualifier.Application.Database.Gap.Queries.GetGapScope
{
    public interface IGetGapScopeQuery
    {
        Task<Object> Execute(int standardId, int evaluationId, int userId, bool scopeToUser = false);
    }
}
