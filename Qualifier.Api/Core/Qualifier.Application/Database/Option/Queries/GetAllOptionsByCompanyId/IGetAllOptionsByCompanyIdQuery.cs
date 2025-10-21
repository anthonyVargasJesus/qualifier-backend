namespace Qualifier.Application.Database.Option.Queries.GetAllOptionsByCompanyId
{
    public interface IGetAllOptionsByCompanyIdQuery
    {
        Task<Object> Execute(int companyId);
    }
}

