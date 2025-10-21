namespace Qualifier.Application.Database.MaturityLevel.Queries.GetAllMaturityLevelsByCompanyId
{
    public interface IGetAllMaturityLevelsByCompanyIdQuery
    {
        Task<Object> Execute(int companyId);
    }
}

