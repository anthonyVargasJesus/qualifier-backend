namespace Qualifier.Application.Database.Role.Queries.GetAllRolesByCompanyId
{
    public interface IGetAllRolesByCompanyIdQuery
    {
        Task<Object> Execute(int companyId);
    }
}

