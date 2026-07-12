using Microsoft.EntityFrameworkCore;
using Qualifier.Application.Database.GapDashboard;
using Qualifier.Common.Application.Service;

namespace Qualifier.Application.Database.GapDashboard.Queries.GetGapDashboard
{
    // Resumen para el dashboard de Inicio (Flutter hoy, Angular más adelante): un solo
    // endpoint con el % de cumplimiento, el desglose por tema/grupo, la distribución por
    // nivel de madurez, el conteo de brechas y algunos ítems pendientes — en vez de que cada
    // cliente arme esas cuentas por su cuenta a partir de las listas completas de evaluación.
    //
    // Misma fórmula de cumplimiento que gap-panel.component.ts (stats/temaRows): "No aplica"
    // y "Pendiente" quedan fuera del % (cumplimiento-de-lo-evaluado, no cobertura). No cambiar
    // sin confirmar con el usuario.
    public class GetGapDashboardQuery : IGetGapDashboardQuery
    {
        private const string PENDIENTE = GapItemsBuilder.PENDIENTE;
        private const string NO_APLICA = GapItemsBuilder.NO_APLICA;
        private const string CUMPLE = GapItemsBuilder.CUMPLE;
        private const int MAX_PENDING_ITEMS = 8;

        private readonly IDatabaseService _databaseService;
        private readonly GapItemsBuilder _itemsBuilder;

        public GetGapDashboardQuery(IDatabaseService databaseService, GapItemsBuilder itemsBuilder)
        {
            _databaseService = databaseService;
            _itemsBuilder = itemsBuilder;
        }

        public async Task<Object> Execute(int standardId, int evaluationId, int userId, bool scopeToUser)
        {
            try
            {
                var (controlItems, scopedControlIds) = await _itemsBuilder.BuildControlItems(standardId, evaluationId, userId, scopeToUser);
                var (requirementItems, scopedRequirementIds) = await _itemsBuilder.BuildRequirementItems(standardId, evaluationId, userId, scopeToUser);

                var allItems = controlItems.Concat(requirementItems).ToList();

                var evaluatedItems = allItems.Where(i => i.estado != PENDIENTE && i.estado != NO_APLICA).ToList();
                var compliantItems = evaluatedItems.Where(i => i.estado == CUMPLE).ToList();
                var pct = evaluatedItems.Count > 0 ? (int)Math.Round(compliantItems.Count * 100.0 / evaluatedItems.Count) : 0;

                var maturityCounts = allItems
                    .GroupBy(i => i.estado)
                    .Select(g => new GetGapDashboardMaturityCountDto { name = g.Key, count = g.Count() })
                    .ToList();

                var themes = allItems
                    .GroupBy(i => i.theme)
                    .Select(g =>
                    {
                        var themeEvaluated = g.Where(i => i.estado != PENDIENTE && i.estado != NO_APLICA).ToList();
                        var themeCompliant = themeEvaluated.Where(i => i.estado == CUMPLE).ToList();
                        return new GetGapDashboardThemeDto
                        {
                            theme = g.Key,
                            totalItems = g.Count(),
                            evaluatedItems = themeEvaluated.Count,
                            compliantItems = themeCompliant.Count,
                            percentage = themeEvaluated.Count > 0
                                ? (int)Math.Round(themeCompliant.Count * 100.0 / themeEvaluated.Count)
                                : 0,
                        };
                    })
                    .ToList();

                var pendingItems = allItems
                    .Where(i => i.estado == PENDIENTE)
                    .OrderBy(i => i.theme).ThenBy(i => i.code)
                    .Take(MAX_PENDING_ITEMS)
                    .Select(i => new GetGapDashboardPendingItemDto
                    {
                        tipo = i.tipo,
                        itemId = i.itemId,
                        code = i.code,
                        name = i.name,
                        theme = i.theme,
                    })
                    .ToList();

                var openBreaches = await (
                    from b in _databaseService.Breach
                    join severity in _databaseService.BreachSeverity on b.breachSeverityId equals severity.breachSeverityId
                    where (b.isDeleted == null || b.isDeleted == false) && b.evaluationId == evaluationId
                        && ((b.type == "2" && scopedControlIds.Contains(b.controlId))
                            || (b.type == "1" && scopedRequirementIds.Contains(b.requirementId)))
                    select new { severity.name, severity.color }
                ).ToListAsync();

                var breachSeverityBreakdown = openBreaches
                    .GroupBy(b => new { b.name, b.color })
                    .Select(g => new GetGapDashboardBreachSeverityDto
                    {
                        name = g.Key.name,
                        color = g.Key.color,
                        count = g.Count(),
                    })
                    .OrderByDescending(s => s.count)
                    .ToList();

                // Mismo criterio de "vencida" que ya usa GetActionPlanProgressQuery / la app
                // móvil (task_list_options.dart): fecha (sin hora) pasada y sin estar en un
                // estado de cierre (abreviatura COMP/CERR).
                var today = DateTime.UtcNow.Date;
                var overdueActionPlansCount = await (
                    from ap in _databaseService.ActionPlan
                    join status in _databaseService.ActionPlanStatus on ap.actionPlanStatusId equals status.actionPlanStatusId
                    where (ap.isDeleted == null || ap.isDeleted == false) && ap.evaluationId == evaluationId
                        && ap.dueDate.Date < today
                        && status.abbreviation != "COMP" && status.abbreviation != "CERR"
                    select ap.actionPlanId
                ).CountAsync();

                var evaluatedItemsWithEvidenceCount = evaluatedItems.Count(i => i.hasEvidence);

                return new GetGapDashboardDto
                {
                    totalItems = allItems.Count,
                    evaluatedItems = evaluatedItems.Count,
                    compliantItems = compliantItems.Count,
                    compliancePercentage = pct,
                    openBreachesCount = openBreaches.Count,
                    overdueActionPlansCount = overdueActionPlansCount,
                    evaluatedItemsWithEvidenceCount = evaluatedItemsWithEvidenceCount,
                    maturityCounts = maturityCounts,
                    themes = themes,
                    pendingItems = pendingItems,
                    breachSeverityBreakdown = breachSeverityBreakdown,
                };
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}
