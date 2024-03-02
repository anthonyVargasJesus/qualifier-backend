namespace Qualifier.Application.Database.MaturityLevel.Queries.GetMaturityLevelsByCompanyId
{
    public interface IGetMaturityLevelsByCompanyIdQuery
    {
        Task<Object> Execute(int skip, int pageSize, string search, int companyId);
    }
}

