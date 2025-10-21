namespace Qualifier.Application.Database.Custodian.Queries.GetAllCustodiansByCompanyId
{
    public interface IGetAllCustodiansByCompanyIdQuery
    {
        Task<Object> Execute(int companyId);
    }
}

