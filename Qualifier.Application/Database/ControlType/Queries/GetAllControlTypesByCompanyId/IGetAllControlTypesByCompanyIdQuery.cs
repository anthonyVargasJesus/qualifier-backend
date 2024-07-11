namespace Qualifier.Application.Database.ControlType.Queries.GetAllControlTypesByCompanyId
{
    public interface IGetAllControlTypesByCompanyIdQuery
    {
        Task<Object> Execute(int companyId);
    }
}

