namespace Qualifier.Application.Database.Personal.Queries.GetAllPersonalsByCompanyId
{
    public interface IGetAllPersonalsByCompanyIdQuery
    {
        Task<Object> Execute(int companyId);
    }
}

