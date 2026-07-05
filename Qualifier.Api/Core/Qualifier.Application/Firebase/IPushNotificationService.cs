namespace Qualifier.Application.Firebase
{
    public interface IPushNotificationService
    {
        Task SendAsync(
            int userId,
            string fcmToken,
            string title,
            string body,
            string type,
            int? actionPlanId = null,
            int? breachId = null,
            int? companyId = null,
            Dictionary<string, string>? data = null);
    }
}
