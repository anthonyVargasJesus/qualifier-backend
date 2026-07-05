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
        // Antes apuntaba al catálogo MAE_RESPONSIBLE (un cargo/rol, sin cuenta de usuario real);
        // ahora se asigna a un usuario real del sistema, para que quede trazable quién ejecuta
        // la acción correctiva. Nullable porque las filas viejas no tienen usuario asignado.
        public int? userId { get; set; }
        public DateTime startDate { get; set; }
        public DateTime dueDate { get; set; }
        public int actionPlanStatusId { get; set; }
        public int actionPlanPriorityId { get; set; }
        public int? companyId { get; set; }
        public UserEntity? user { get; set; }
        public ActionPlanStatusEntity? actionPlanStatus { get; set; }
        public ActionPlanPriorityEntity? actionPlanPriority { get; set; }
    }
}

