namespace Qualifier.Application.Database.ValuationInActive.Queries.GetAllValuationInActivesByCompanyId
{
    public interface IGetAllValuationInActivesByCompanyIdQuery
    {
        Task<Object> Execute(int companyId);
    }
}
