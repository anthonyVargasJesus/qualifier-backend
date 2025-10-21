namespace Qualifier.Application.Database.ValuationInActive.Commands.CreateValuationInActive
{
    public interface ICreateValuationInActiveCommand
    {
        Task<Object> Execute(CreateValuationInActiveDto model);
    }
}

