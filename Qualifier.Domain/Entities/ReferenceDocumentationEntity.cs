using Qualifier.Common.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Qualifier.Domain.Entities
{
    public class ReferenceDocumentationEntity : BaseEntity
    {
        public long referenceDocumentationId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int documentationId { get; set; }
        public long? requirementEvaluationId { get; set; }
        public long? controlEvaluationId { get; set; }
        public int evaluationId { get; set; }
        public int companyId { get; set; }
        public DocumentationEntity documentation { get; set; }
        public RequirementEvaluationEntity requirementEvaluation { get; set; }
        [NotMapped]
        public RequirementEntity? requirement { get; set; }
        [NotMapped]
        public ControlEntity? control { get; set; }
    }
}

