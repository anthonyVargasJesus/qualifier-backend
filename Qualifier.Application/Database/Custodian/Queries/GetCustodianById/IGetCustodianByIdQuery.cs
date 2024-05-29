namespace Qualifier.Application.Database.Custodian.Queries.GetCustodianById
{
    public interface IGetCustodianByIdQuery
    {
        Task<Object> Execute(int custodianId);
    }
}

