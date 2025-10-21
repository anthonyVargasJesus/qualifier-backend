namespace Qualifier.Application.Database.Reviewer.Queries.GetReviewerById
{
    public class GetReviewerByIdDto
    {
        public int reviewerId { get; set; }
        public int personalId { get; set; }
        public int responsibleId { get; set; }
        public int versionId { get; set; }
        public int documentationId { get; set; }
        public int companyId { get; set; }

    }
}

