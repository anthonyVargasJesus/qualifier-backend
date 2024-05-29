namespace Qualifier.Application.Database.ActivesInventory.Commands.UpdateActivesInventory
{
    public interface IUpdateActivesInventoryCommand
    {
        Task<Object> Execute(UpdateActivesInventoryDto model, int id);
    }
}

