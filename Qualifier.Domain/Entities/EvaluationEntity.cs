using Qualifier.Common.Domain.Entities;

namespace Qualifier.Domain.Entities
{
    public class EvaluationEntity : BaseEntity
    {
        public int evaluationId { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public string description { get; set; }
        public int standardId { get; set; }
        public int companyId { get; set; }
        public StandardEntity standard { get; set; }
        public ICollection<RequirementEvaluationEntity> requirementEvaluations { get; set; }
        public ICollection<ControlEvaluationEntity> controlEvaluations { get; set; }
    }
}

