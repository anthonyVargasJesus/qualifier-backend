namespace Qualifier.Application.Database.ControlGroup.Queries.GetControlGroupsByCompanyId
{
    public interface IGetControlGroupsByCompanyIdQuery
    {
        Task<Object> Execute(int skip, int pageSize, string search, int companyId);
    }
}

