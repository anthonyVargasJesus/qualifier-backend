namespace Qualifier.Application.Database.Gap.Queries.GetGapSummary
{
    public interface IGetGapSummaryQuery
    {
        Task<Object> Execute(int companyId, int userId, bool scopeToUser);
    }
}
