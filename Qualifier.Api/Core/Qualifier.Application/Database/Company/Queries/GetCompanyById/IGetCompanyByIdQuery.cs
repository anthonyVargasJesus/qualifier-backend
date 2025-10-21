namespace Qualifier.Application.Database.Company.Queries.GetCompanyById
{
    public interface IGetCompanyByIdQuery
    {
        Task<Object> Execute(int companyId);
    }
}

