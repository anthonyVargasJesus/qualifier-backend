namespace Qualifier.Application.Database.DefaultRisk.Queries.GetDefaultRiskById
{
    public class GetDefaultRiskByIdDto
    {
        public int defaultRiskId { get; set; }
        public int? standardId { get; set; }
        public string name { get; set; }
        public int menaceId { get; set; }
        public int vulnerabilityId { get; set; }

    }
}

