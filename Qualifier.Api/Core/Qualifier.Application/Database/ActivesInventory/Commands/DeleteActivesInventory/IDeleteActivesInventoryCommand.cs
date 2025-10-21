namespace Qualifier.Application.Database.ActivesInventory.Commands.DeleteActivesInventory
{
    public interface IDeleteActivesInventoryCommand
    {
        Task<Object> Execute(int id, int updateUserId);
    }
}

