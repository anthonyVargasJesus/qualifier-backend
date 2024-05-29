namespace Qualifier.Application.Database.ActivesInventory.Queries.GetActivesInventoriesByCompanyId
{
    public interface IGetActivesInventoriesByCompanyIdQuery
    {
        Task<Object> Execute(int skip, int pageSize, string search, int companyId);
    }
}

