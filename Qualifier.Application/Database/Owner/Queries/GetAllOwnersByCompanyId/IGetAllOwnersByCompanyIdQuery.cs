namespace Qualifier.Application.Database.Owner.Queries.GetAllOwnersByCompanyId
{
    public interface IGetAllOwnersByCompanyIdQuery
    {
        Task<Object> Execute(int companyId);
    }
}

