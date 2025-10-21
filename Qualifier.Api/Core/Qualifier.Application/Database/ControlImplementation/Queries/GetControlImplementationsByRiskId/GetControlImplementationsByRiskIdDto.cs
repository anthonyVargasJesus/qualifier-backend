namespace Qualifier.Application.Database.ControlImplementation.Queries.GetControlImplementationsByRiskId
{
    public class GetControlImplementationsByRiskIdDto
    {
        public int controlImplementationId { get; set; }
        public string activities { get; set; } = string.Empty;
        public DateTime startDate { get; set; }
        public DateTime? verificationDate { get; set; }
        public int responsibleId { get; set; }
        public string? observation { get; set; }
        public bool? isImplemented { get; set; }
        public bool? isEffective { get; set; }
        public GetControlImplementationsByRiskIdResponsibleDto? responsible { get; set; }
        public GetControlImplementationsByRiskIdRiskDto? risk { get; set; }
    }
    public class GetControlImplementationsByRiskIdResponsibleDto
    {
        public string name { get; set; } = string.Empty;

    }
    public class GetControlImplementationsByRiskIdRiskDto
    {
        public string name { get; set; } = string.Empty;

    }
}


