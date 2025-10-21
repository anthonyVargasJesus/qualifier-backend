namespace Qualifier.Application.Database.Location.Queries.GetAllLocationsByCompanyId
{
    public interface IGetAllLocationsByCompanyIdQuery
    {
        Task<Object> Execute(int companyId);
    }
}

