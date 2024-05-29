namespace Qualifier.Application.Database.Owner.Queries.GetOwnersByCompanyId
{
    public interface IGetOwnersByCompanyIdQuery
    {
        Task<Object> Execute(int skip, int pageSize, string search, int companyId);
    }
}

