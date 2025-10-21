namespace Qualifier.Application.Database.Menu.Queries.GetAllMenusByCompanyId
{
    public interface IGetAllMenusByCompanyIdQuery
    {
        Task<Object> Execute(int companyId);
    }
}

