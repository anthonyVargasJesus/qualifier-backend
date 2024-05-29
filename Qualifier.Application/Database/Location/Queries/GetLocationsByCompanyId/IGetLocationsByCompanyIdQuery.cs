namespace Qualifier.Application.Database.Location.Queries.GetLocationsByCompanyId
{
    public interface IGetLocationsByCompanyIdQuery
    {
        Task<Object> Execute(int skip, int pageSize, string search, int companyId);
    }
}

