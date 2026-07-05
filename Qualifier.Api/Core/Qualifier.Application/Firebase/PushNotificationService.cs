using FirebaseAdmin.Messaging;
using Microsoft.Extensions.Configuration;

namespace Qualifier.Application.Firebase
{
    public class PushNotificationService : IPushNotificationService
    {
        private readonly IConfiguration _configuration;

        public PushNotificationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

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
                FirebaseManager.configCredentials(_configuration["FIREBASE_SERVICE_ACCOUNT_JSON"]);
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
