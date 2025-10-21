using Qualifier.Common.Domain.Entities;

namespace Qualifier.Domain.Entities
{
    public class ActionPlanEntity : BaseEntity
    {
        public int actionPlanId { get; set; }
        public int breachId { get; set; }
        public int evaluationId { get; set; }
        public int standardId { get; set; }
        public string title { get; set; } = string.Empty;
        public string? description { get; set; }
        public int responsibleId { get; set; }
        public DateTime startDate { get; set; }
        public DateTime dueDate { get; set; }
        public int actionPlanStatusId { get; set; }
        public int actionPlanPriorityId { get; set; }
        public int? companyId { get; set; }
        public ResponsibleEntity? responsible { get; set; }
        public ActionPlanStatusEntity? actionPlanStatus { get; set; }
        public ActionPlanPriorityEntity? actionPlanPriority { get; set; }
    }
}

