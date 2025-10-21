namespace Qualifier.Application.Database.UserState.Queries.GetAllUserStatesByCompanyId
{
    public interface IGetAllUserStatesByCompanyIdQuery
    {
        Task<Object> Execute(int companyId);
    }
}

