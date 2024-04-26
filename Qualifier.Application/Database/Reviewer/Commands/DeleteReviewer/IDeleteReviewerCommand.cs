namespace Qualifier.Application.Database.Reviewer.Commands.DeleteReviewer
{
    public interface IDeleteReviewerCommand
    {
        Task<Object> Execute(int id, int updateUserId);
    }
}

