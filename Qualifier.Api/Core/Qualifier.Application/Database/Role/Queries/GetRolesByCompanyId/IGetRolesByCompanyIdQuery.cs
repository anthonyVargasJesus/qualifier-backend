namespace Qualifier.Application.Database.Role.Queries.GetRolesByCompanyId
{
    public interface IGetRolesByCompanyIdQuery
    {
        Task<Object> Execute(int skip, int pageSize, string search, int companyId);
    }
}

