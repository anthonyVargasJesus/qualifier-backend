namespace Qualifier.Application.Database.Custodian.Queries.GetCustodiansByCompanyId
{
    public interface IGetCustodiansByCompanyIdQuery
    {
        Task<Object> Execute(int skip, int pageSize, string search, int companyId);
    }
}

