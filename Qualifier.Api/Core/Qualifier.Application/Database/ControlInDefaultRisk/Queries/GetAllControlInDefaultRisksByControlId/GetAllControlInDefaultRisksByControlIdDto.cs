namespace Qualifier.Application.Database.ControlInDefaultRisk.Queries.GetAllControlInDefaultRisksByControlId
{
    public class GetAllControlInDefaultRisksByControlIdDto
    {
        public int controlInDefaultRiskId { get; set; }
        public int defaultRiskId { get; set; }
        public int controlId { get; set; }
        public bool isActive { get; set; }
        public GetAllControlInDefaultRisksByControlIdDefaultRiskDto? defaultRisk { get; set; }
    }
    public class GetAllControlInDefaultRisksByControlIdDefaultRiskDto
    {
        public string name { get; set; }

    }
}

