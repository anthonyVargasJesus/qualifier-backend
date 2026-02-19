using Qualifier.Common.Domain.Entities;

namespace Qualifier.Domain.Entities
{
    public class ResponsibleEntity : BaseEntity
    {
        public int responsibleId { get; set; }
        public string name { get; set; }
        public string? description { get; set; }
        public int standardId { get; set; }
        public int companyId { get; set; }
        public ICollection<RequirementEvaluationEntity> requirementEvaluations { get; set; }
        public ICollection<ControlEvaluationEntity> controlEvaluations { get; set; }
    }
}

