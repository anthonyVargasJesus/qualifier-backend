namespace Qualifier.Application.Database.UsageClassification.Queries.GetUsageClassificationById
{
    public interface IGetUsageClassificationByIdQuery
    {
        Task<Object> Execute(int usageClassificationId);
    }
}

