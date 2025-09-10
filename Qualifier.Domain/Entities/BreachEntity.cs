using Qualifier.Common.Domain.Entities;

namespace Qualifier.Domain.Entities
{
    public class BreachEntity : BaseEntity
    {
        public int breachId { get; set; }
        public int evaluationId { get; set; }
        public int standardId { get; set; }
        public string? type { get; set; }
        public int requirementId { get; set; }
        public int controlId { get; set; }
        public string? numerationToShow { get; set; }      
        public string title { get; set; }
        public string description { get; set; }
        public int breachSeverityId { get; set; }
        public int breachStatusId { get; set; }
        public int responsibleId { get; set; }
        public string? evidenceDescription { get; set; }
        public int? companyId { get; set; }
        public EvaluationEntity evaluation { get; set; }
        public BreachSeverityEntity breachSeverity { get; set; }
        public BreachStatusEntity breachStatus { get; set; }
        public ResponsibleEntity responsible { get; set; }
        public RequirementEntity? requirement { get; set; }
        public ControlEntity? control { get; set; }
    }
}

