using Qualifier.Common.Domain.Entities;

namespace Qualifier.Domain.Entities
{
    public class RiskAssessmentEntity : BaseEntity
    {
        public int riskAssessmentId { get; set; }
        public int riskId { get; set; }
        public decimal valuationCID { get; set; }
        public decimal menaceLevelValue { get; set; }
        public decimal vulnerabilityLevelValue { get; set; }
        public string existingImplementedControls { get; set; }
        public decimal riskAssessmentValue { get; set; }
        public int riskLevelId { get; set; }
        public int? companyId { get; set; }
        public RiskEntity risk { get; set; }
        public RiskLevelEntity riskLevel { get; set; }
    }
}

