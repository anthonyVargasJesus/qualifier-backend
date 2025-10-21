
namespace Qualifier.Application.Database.Breach.Queries.GetRisksIdentification
{
    public interface IGetRisksIdentificationQuery
    {
        Task<Object> Execute(int skip, int pageSize, string search, int companyId);
    }
}
