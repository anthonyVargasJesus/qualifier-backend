using Microsoft.EntityFrameworkCore;
using Qualifier.Application.Database.Evaluation.Queries.GetCurrentEvaluation;
using Qualifier.Application.Database.GapDashboard.Queries.GetGapDashboard;
using Qualifier.Application.Database.Notifications.Queries.GetNotificationsByUserId;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;

namespace Qualifier.Application.Database.GapDashboard.Queries.GetHomeDashboardBootstrap
{
    public class GetHomeDashboardBootstrapQuery : IGetHomeDashboardBootstrapQuery
    {
        private readonly IGetCurrentEvaluationQuery _getCurrentEvaluationQuery;
        private readonly IGetGapDashboardQuery _getGapDashboardQuery;
        private readonly IGetNotificationsByUserIdQuery _getNotificationsByUserIdQuery;
        private readonly IDatabaseService _databaseService;

        public GetHomeDashboardBootstrapQuery(
            IGetCurrentEvaluationQuery getCurrentEvaluationQuery,
            IGetGapDashboardQuery getGapDashboardQuery,
            IGetNotificationsByUserIdQuery getNotificationsByUserIdQuery,
            IDatabaseService databaseService)
        {
            _getCurrentEvaluationQuery = getCurrentEvaluationQuery;
            _getGapDashboardQuery = getGapDashboardQuery;
            _getNotificationsByUserIdQuery = getNotificationsByUserIdQuery;
            _databaseService = databaseService;
        }

        public async Task<Object> Execute(int userId, int notificationsPageSize = 3)
        {
            try
            {
                // IDatabaseService es Scoped: todas las queries de este request
                // comparten el mismo DbContext, que no soporta operaciones
                // concurrentes — await secuencial, no Task.WhenAll (mismo motivo
                // que en los demás endpoints bootstrap de esta sesión).
                var evalResult = await _getCurrentEvaluationQuery.Execute(0);
                if (evalResult is BaseErrorResponseDto) return evalResult;
                if (evalResult is not GetCurrentEvaluationDto evaluation) return BaseApplication.getExceptionErrorResponse();

                var dashboardResult = await _getGapDashboardQuery.Execute(evaluation.standardId, evaluation.evaluationId, userId, true);
                if (dashboardResult is BaseErrorResponseDto) return dashboardResult;
                if (dashboardResult is not GetGapDashboardDto dashboard) return BaseApplication.getExceptionErrorResponse();

                var notifResult = await _getNotificationsByUserIdQuery.Execute(userId, 0, notificationsPageSize);
                if (notifResult is BaseErrorResponseDto) return notifResult;
                if (notifResult is not BaseResponseDto<GetNotificationsByUserIdDto> notifResponse) return BaseApplication.getExceptionErrorResponse();

                // GetNotificationsByUserIdQuery guarda el total de no leídas dentro
                // de un objeto anónimo (pagination.unreadCount) pensado para el
                // listado paginado completo, no para esta vista previa de 3 — se
                // recalcula acá con la misma condición para no depender de un tipo
                // anónimo ajeno a esta query.
                var unreadNotificationsCount = await _databaseService.Notification
                    .CountAsync(n => n.userId == userId && (n.isDeleted == null || n.isDeleted == false) && !n.isRead);

                var response = new GetHomeDashboardBootstrapDto
                {
                    dashboard = dashboard,
                    notifications = notifResponse.data,
                    unreadNotificationsCount = unreadNotificationsCount,
                };
                return response;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }
    }
}
