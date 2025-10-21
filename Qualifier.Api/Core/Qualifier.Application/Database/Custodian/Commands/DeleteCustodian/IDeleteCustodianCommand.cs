namespace Qualifier.Application.Database.Custodian.Commands.DeleteCustodian
{
    public interface IDeleteCustodianCommand
    {
        Task<Object> Execute(int id, int updateUserId);
    }
}

