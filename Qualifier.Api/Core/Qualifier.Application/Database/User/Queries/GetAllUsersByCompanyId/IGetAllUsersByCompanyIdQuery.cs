namespace Qualifier.Application.Database.User.Queries.GetAllUsersByCompanyId
{
    public interface IGetAllUsersByCompanyIdQuery
    {
        Task<Object> Execute(int companyId);
    }
}

