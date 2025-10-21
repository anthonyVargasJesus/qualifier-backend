namespace Qualifier.Application.Database.Custodian.Commands.CreateCustodian
{
    public interface ICreateCustodianCommand
    {
        Task<Object> Execute(CreateCustodianDto model);
    }
}

