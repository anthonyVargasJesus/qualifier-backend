namespace Qualifier.Application.Database.Gap.Queries.GetGapItems
{
    public interface IGetGapItemsQuery
    {
        Task<Object> Execute(int userId, bool scopeToUser, int skip, int pageSize, string search, string theme);
    }
}
