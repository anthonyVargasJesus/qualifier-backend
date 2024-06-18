namespace Qualifier.Application.Database.Option.Queries.GetOptionsByCompanyId
{
    public interface IGetOptionsByCompanyIdQuery
    {
        Task<Object> Execute(int skip, int pageSize, string search, int companyId);
    }
}

