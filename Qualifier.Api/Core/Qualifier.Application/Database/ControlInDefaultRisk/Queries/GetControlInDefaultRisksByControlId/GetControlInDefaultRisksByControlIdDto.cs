namespace Qualifier.Application.Database.ControlInDefaultRisk.Queries.GetControlInDefaultRisksByControlId
{
    public class GetControlInDefaultRisksByControlIdDto
    {
        public int controlInDefaultRiskId { get; set; }
        public int defaultRiskId { get; set; }
        public int controlId { get; set; }
        public bool isActive { get; set; }
        public GetControlInDefaultRisksByControlIdDefaultRiskDto? defaultRisk { get; set; }
    }
    public class GetControlInDefaultRisksByControlIdDefaultRiskDto
    {
        public string name { get; set; }

    }
}

