namespace Qualifier.Application.Database.ActivesInventory.Commands.CreateActivesInventory
{
    public interface ICreateActivesInventoryCommand
    {
        Task<Object> Execute(CreateActivesInventoryDto model);
    }
}

