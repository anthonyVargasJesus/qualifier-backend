namespace Qualifier.Application.Database.UsageClassification.Queries.GetAllUsageClassificationsByCompanyId
{
    public interface IGetAllUsageClassificationsByCompanyIdQuery
    {
        Task<Object> Execute(int companyId);
    }
}

