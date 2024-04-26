namespace Qualifier.Application.Database.Reviewer.Commands.UpdateReviewer
{
    public interface IUpdateReviewerCommand
    {
        Task<Object> Execute(UpdateReviewerDto model, int id);
    }
}

