namespace Qualifier.Application.Database.Subprocess.Queries.GetSubprocesssByCompanyId
{
    public interface IGetSubprocesssByCompanyIdQuery
    {
        Task<Object> Execute(int skip, int pageSize, string search, int companyId);
    }
}

