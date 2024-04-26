namespace Qualifier.Application.Database.Reviewer.Queries.GetAllReviewersByVersionId
{
    public interface IGetAllReviewersByVersionIdQuery
    {
        Task<Object> Execute(int versionId);
    }
}

