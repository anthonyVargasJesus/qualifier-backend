namespace Qualifier.Application.Database.MenaceType.Queries.GetMenaceTypesByCompanyId
{
    public interface IGetMenaceTypesByCompanyIdQuery
    {
        Task<Object> Execute(int skip, int pageSize, string search, int companyId);
    }
}

