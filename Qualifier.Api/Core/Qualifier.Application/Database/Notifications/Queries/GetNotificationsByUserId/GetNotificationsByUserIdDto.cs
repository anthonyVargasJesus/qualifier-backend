namespace Qualifier.Application.Database.Notifications.Queries.GetNotificationsByUserId
{
    public class GetNotificationsByUserIdDto
    {
        public int notificationId { get; set; }
        public string title { get; set; } = string.Empty;
        public string body { get; set; } = string.Empty;
        public string? type { get; set; }
        public int? actionPlanId { get; set; }
        public int? breachId { get; set; }
        public bool isRead { get; set; }
        public DateTime? creationDate { get; set; }
    }
}
