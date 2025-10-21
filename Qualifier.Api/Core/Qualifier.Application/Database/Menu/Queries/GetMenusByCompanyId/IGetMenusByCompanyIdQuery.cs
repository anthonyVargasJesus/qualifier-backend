namespace Qualifier.Application.Database.Menu.Queries.GetMenusByCompanyId
{
    public interface IGetMenusByCompanyIdQuery
    {
        Task<Object> Execute(int skip, int pageSize, string search, int companyId);
    }
}

