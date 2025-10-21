namespace Qualifier.Application.Database.ValuationInActive.Commands.UpdateValuationInActive
{
    public interface IUpdateValuationInActiveCommand
    {
        Task<Object> Execute(UpdateValuationInActiveDto model, int id);
    }
}

