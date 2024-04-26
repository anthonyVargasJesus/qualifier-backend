namespace Qualifier.Application.Database.Personal.Queries.GetPersonalsByCompanyId
{
    public interface IGetPersonalsByCompanyIdQuery
    {
        Task<Object> Execute(int skip, int pageSize, string search, int companyId);
    }
}

