using Qualifier.Common.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Qualifier.Domain.Entities
{
    public class RequirementEvaluationEntity : BaseEntity
    {
        public long requirementEvaluationId { get; set; }
        public int evaluationId { get; set; }
        public int requirementId { get; set; }
        public int maturityLevelId { get; set; }
        public decimal value { get; set; }
        public int responsibleId { get; set; }
        public string justification { get; set; }
        public string improvementActions { get; set; }
        public int standardId { get; set; }
        public int companyId { get; set; }
        public EvaluationEntity evaluation { get; set; }
        public RequirementEntity requirement { get; set; }
        public MaturityLevelEntity maturityLevel { get; set; }
        public ResponsibleEntity responsible { get; set; }
        [NotMapped]
        public List<int> arrayReferenceDocumentations { get; set; }
        [NotMapped]
        public List<MaturityLevelEntity> values { get; set; }

        [NotMapped]
        public List<ReferenceDocumentationEntity> referenceDocumentations { get; set; }
        [NotMapped]
        public List<DocumentationEntity> documentation { get; set; }
    }
}

