namespace Qualifier.Application.Firebase
{
    public interface IPushNotificationService
    {
        Task SendAsync(string fcmToken, string title, string body, Dictionary<string, string>? data = null);
    }
}
