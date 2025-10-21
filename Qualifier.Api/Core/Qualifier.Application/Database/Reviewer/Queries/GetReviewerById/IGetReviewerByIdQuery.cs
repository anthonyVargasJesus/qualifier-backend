namespace Qualifier.Application.Database.Reviewer.Queries.GetReviewerById
{
    public interface IGetReviewerByIdQuery
    {
        Task<Object> Execute(int reviewerId);
    }
}

