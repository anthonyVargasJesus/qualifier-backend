using FirebaseAdmin.Messaging;

namespace Qualifier.Application.Firebase
{
    public class PushNotificationService : IPushNotificationService
    {
        public async Task SendAsync(string fcmToken, string title, string body, Dictionary<string, string>? data = null)
        {
            var message = new Message
            {
                Token = fcmToken,
                Notification = new Notification { Title = title, Body = body },
                Data = data,
            };

            try
            {
                FirebaseManager.configCredentials();
                await FirebaseMessaging.DefaultInstance.SendAsync(message);
            }
            catch (Exception)
            {
                // Best-effort: un push fallido (token inválido/expirado, credencial no
                // configurada) no debe romper la operación que lo dispara (ej. crear un plan
                // de acción).
            }
        }
    }
}
