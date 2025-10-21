namespace Qualifier.Application.Database.Reviewer.Commands.CreateReviewer
{
    public interface ICreateReviewerCommand
    {
        Task<Object> Execute(CreateReviewerDto model);
    }
}

