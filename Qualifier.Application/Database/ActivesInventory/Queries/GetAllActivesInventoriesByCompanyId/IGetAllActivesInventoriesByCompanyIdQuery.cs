namespace Qualifier.Application.Database.ActivesInventory.Queries.GetAllActivesInventoriesByCompanyId
{
    public interface IGetAllActivesInventoriesByCompanyIdQuery
    {
        Task<Object> Execute(int companyId);
    }
}

