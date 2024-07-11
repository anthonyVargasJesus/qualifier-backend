namespace Qualifier.Application.Database.ControlType.Queries.GetControlTypesByCompanyId
{
    public interface IGetControlTypesByCompanyIdQuery
    {
        Task<Object> Execute(int skip, int pageSize, string search, int companyId);
    }
}

