namespace Qualifier.Application.Database.Subprocess.Queries.GetAllSubprocesssByCompanyId
{
    public interface IGetAllSubprocesssByCompanyIdQuery
    {
        Task<Object> Execute(int companyId);
    }
}

