using FirebaseAdmin.Messaging;
using Microsoft.Extensions.Configuration;
using Qualifier.Application.Database;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Firebase
{
    public class PushNotificationService : IPushNotificationService
    {
        private readonly IConfiguration _configuration;
        private readonly IDatabaseService _databaseService;

        public PushNotificationService(IConfiguration configuration, IDatabaseService databaseService)
        {
            _configuration = configuration;
            _databaseService = databaseService;
        }

        public async Task SendAsync(
            int userId,
            string fcmToken,
            string title,
            string body,
            string type,
            int? actionPlanId = null,
            int? breachId = null,
            int? companyId = null,
            Dictionary<string, string>? data = null)
        {
            // Guarda el historial primero: esto debe quedar registrado aunque el envío del push
            // en sí falle (token inválido/expirado, credencial no configurada, etc.).
            try
            {
                await _databaseService.Notification.AddAsync(new NotificationEntity
                {
                    userId = userId,
                    title = title,
                    body = body,
                    type = type,
                    actionPlanId = actionPlanId,
                    breachId = breachId,
                    companyId = companyId,
                    creationDate = DateTime.UtcNow,
                });
                await _databaseService.SaveAsync();
            }
            catch (Exception)
            {
                // Best-effort: no debe romper la operación que dispara la notificación.
            }

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
