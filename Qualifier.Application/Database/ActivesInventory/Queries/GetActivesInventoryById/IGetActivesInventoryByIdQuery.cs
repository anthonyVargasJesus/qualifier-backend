namespace Qualifier.Application.Database.ActivesInventory.Queries.GetActivesInventoryById
{
    public interface IGetActivesInventoryByIdQuery
    {
        Task<Object> Execute(int activesInventoryId);
    }
}

