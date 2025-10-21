namespace Qualifier.Application.Database.ValuationInActive.Commands.DeleteValuationInActive
{
    public interface IDeleteValuationInActiveCommand
    {
        Task<Object> Execute(int id, int updateUserId);
    }
}

