namespace Qualifier.Application.Database.ControlGroup.Queries.GetAllControlGroupsByCompanyId
{
    public interface IGetAllControlGroupsByCompanyIdQuery
    {
        Task<Object> Execute(int companyId);
    }
}

