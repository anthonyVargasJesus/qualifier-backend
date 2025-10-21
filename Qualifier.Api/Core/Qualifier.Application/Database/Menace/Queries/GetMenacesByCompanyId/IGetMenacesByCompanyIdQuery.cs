namespace Qualifier.Application.Database.Menace.Queries.GetMenacesByCompanyId
{
    public interface IGetMenacesByCompanyIdQuery
    {
        Task<Object> Execute(int skip, int pageSize, string search, int companyId);
    }
}

