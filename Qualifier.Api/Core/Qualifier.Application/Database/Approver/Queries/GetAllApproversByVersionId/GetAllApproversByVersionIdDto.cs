namespace Qualifier.Application.Database.Approver.Queries.GetAllApproversByVersionId
{
    public class GetAllApproversByVersionIdDto
    {
        public int approverId { get; set; }
        public int personalId { get; set; }
        public int responsibleId { get; set; }
        public GetAllApproversByVersionIdPersonalDto? personal { get; set; }
        public GetAllApproversByVersionIdResponsibleDto? responsible { get; set; }
    }
    public class GetAllApproversByVersionIdPersonalDto
    {
        public string name { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }

    }
    public class GetAllApproversByVersionIdResponsibleDto
    {
        public string name { get; set; }

    }
}

