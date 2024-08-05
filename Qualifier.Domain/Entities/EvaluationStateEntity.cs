using Qualifier.Common.Domain.Entities;

namespace Qualifier.Domain.Entities
{
    public class EvaluationStateEntity : BaseEntity
    {
        public int evaluationStateId { get; set; }
        public string name { get; set; }
        public int value { get; set; }
        public string? color { get; set; }
        public int? companyId { get; set; }
    }
}

