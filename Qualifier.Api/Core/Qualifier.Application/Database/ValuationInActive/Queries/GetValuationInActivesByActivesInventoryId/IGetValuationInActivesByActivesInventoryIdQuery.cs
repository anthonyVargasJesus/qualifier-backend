namespace Qualifier.Application.Database.ValuationInActive.Queries.GetValuationInActivesByActivesInventoryId
{
    public interface IGetValuationInActivesByActivesInventoryIdQuery
    {
        Task<Object> Execute(int skip, int pageSize, string search, int activesInventoryId);
    }
}

