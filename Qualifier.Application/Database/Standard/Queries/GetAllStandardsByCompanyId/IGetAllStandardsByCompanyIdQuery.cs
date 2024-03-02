namespace Qualifier.Application.Database.Standard.Queries.GetAllStandardsByCompanyId
{
    public interface IGetAllStandardsByCompanyIdQuery
    {
        Task<Object> Execute(int companyId);
    }
}

