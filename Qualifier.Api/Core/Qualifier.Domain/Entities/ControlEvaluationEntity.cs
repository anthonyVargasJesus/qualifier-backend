using Qualifier.Common.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Qualifier.Domain.Entities
{
    public class ControlEvaluationEntity : BaseEntity
    {
        public long controlEvaluationId { get; set; }
        public int evaluationId { get; set; }
        public int controlId { get; set; }
        public int maturityLevelId { get; set; }
        public decimal value { get; set; }
        public int responsibleId { get; set; }
        public string justification { get; set; }
        public string improvementActions { get; set; }
        public string controlDescription { get; set; }
        public string controlType { get; set; }
        public int standardId { get; set; }
        public int companyId { get; set; }
        public EvaluationEntity evaluation { get; set; }
        public ControlEntity control { get; set; }
        public MaturityLevelEntity maturityLevel { get; set; }
        public ResponsibleEntity responsible { get; set; }
        [NotMapped]
        public List<int> arrayReferenceDocumentations { get; set; }
        [NotMapped]
        public int controlGroupId { get; set; }
        [NotMapped]
        public List<ReferenceDocumentationEntity> referenceDocumentations { get; set; }

        [NotMapped]
        public string? state { get; set; }
        [NotMapped]
        public decimal? percentage { get; set; }
    }
}

