namespace Qualifier.Application.Database.Approver.Queries.GetApproverById
{
    public class GetApproverByIdDto
    {
        public int approverId { get; set; }
        public int personalId { get; set; }
        public int responsibleId { get; set; }
        public int versionId { get; set; }
        public int documentationId { get; set; }
        public int companyId { get; set; }

    }
}

