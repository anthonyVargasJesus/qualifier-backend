namespace Qualifier.Application.Database.ConfidentialityLevel.Queries.GetAllConfidentialityLevelsByCompanyId
{
    public interface IGetAllConfidentialityLevelsByCompanyIdQuery
    {
        Task<Object> Execute(int companyId);
    }
}

