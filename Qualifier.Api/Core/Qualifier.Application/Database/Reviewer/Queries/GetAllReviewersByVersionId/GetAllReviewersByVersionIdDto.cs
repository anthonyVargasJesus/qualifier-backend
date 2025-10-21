namespace Qualifier.Application.Database.Reviewer.Queries.GetAllReviewersByVersionId
{
    public class GetAllReviewersByVersionIdDto
    {
        public int reviewerId { get; set; }
        public int personalId { get; set; }
        public int responsibleId { get; set; }
        public GetAllReviewersByVersionIdPersonalDto personal { get; set; }
        public GetAllReviewersByVersionIdResponsibleDto responsible { get; set; }

    }

    public class GetAllReviewersByVersionIdPersonalDto
    {
        public string name { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }

    }
    public class GetAllReviewersByVersionIdResponsibleDto
    {
        public string name { get; set; }

    }

}
