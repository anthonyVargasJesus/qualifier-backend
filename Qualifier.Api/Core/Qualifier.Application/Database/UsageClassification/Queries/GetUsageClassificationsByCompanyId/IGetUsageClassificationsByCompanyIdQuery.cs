namespace Qualifier.Application.Database.UsageClassification.Queries.GetUsageClassificationsByCompanyId
{
    public interface IGetUsageClassificationsByCompanyIdQuery
    {
        Task<Object> Execute(int skip, int pageSize, string search, int companyId);
    }
}

