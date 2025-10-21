namespace Qualifier.Application.Database.ActiveType.Queries.GetAllActiveTypesByCompanyId
{
    public interface IGetAllActiveTypesByCompanyIdQuery
    {
        Task<Object> Execute(int companyId);
    }
}

