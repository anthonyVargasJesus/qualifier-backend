namespace Qualifier.Application.Database.Custodian.Commands.UpdateCustodian
{
    public interface IUpdateCustodianCommand
    {
        Task<Object> Execute(UpdateCustodianDto model, int id);
    }
}

