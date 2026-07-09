using Qualifier.Application.Database.GapDashboard.Queries.GetGapDashboard;
using Qualifier.Application.Database.Notifications.Queries.GetNotificationsByUserId;

namespace Qualifier.Application.Database.GapDashboard.Queries.GetHomeDashboardBootstrap
{
    // Todo lo que HomeDashboardPage necesita para cargar, en una sola
    // respuesta — reemplaza 3 llamadas HTTP secuenciales del frontend
    // (evaluación actual, dashboard de cumplimiento, vista previa de
    // notificaciones) por una sola. La evaluación actual se resuelve del
    // lado del servidor solo para poder pedir el dashboard; el frontend
    // nunca necesitó el objeto evaluación en sí.
    public class GetHomeDashboardBootstrapDto
    {
        public GetGapDashboardDto dashboard { get; set; } = null!;
        public List<GetNotificationsByUserIdDto> notifications { get; set; } = new();
        public int unreadNotificationsCount { get; set; }
    }
}
