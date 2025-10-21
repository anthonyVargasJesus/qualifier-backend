namespace Qualifier.Application.Database.ControlInDefaultRisk.Queries.GetControlInDefaultRiskById
{
    public class GetControlInDefaultRiskByIdDto
    {
        public int controlInDefaultRiskId { get; set; }
        public int defaultRiskId { get; set; }
        public int controlId { get; set; }
        public bool? isActive { get; set; }

    }
}

