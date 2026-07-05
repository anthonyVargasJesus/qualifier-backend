using Qualifier.Common.Domain.Entities;

namespace Qualifier.Domain.Entities
{
    public class NotificationEntity : BaseEntity
    {
        public int notificationId { get; set; }
        public int userId { get; set; }
        public string title { get; set; } = string.Empty;
        public string body { get; set; } = string.Empty;
        public string? type { get; set; }
        public int? actionPlanId { get; set; }
        public int? breachId { get; set; }
        public int? companyId { get; set; }
    }
}
