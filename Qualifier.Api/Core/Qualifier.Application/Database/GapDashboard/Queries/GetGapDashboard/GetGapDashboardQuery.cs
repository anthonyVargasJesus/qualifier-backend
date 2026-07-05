using Microsoft.EntityFrameworkCore;
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
        private const string PENDIENTE = "Pendiente";
        private const string NO_APLICA = "No aplica";
        private const string CUMPLE = "Cumple";
        private const int MAX_PENDING_ITEMS = 8;

        private readonly IDatabaseService _databaseService;

        public GetGapDashboardQuery(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public async Task<Object> Execute(int standardId, int evaluationId, int userId, bool scopeToUser)
        {
            try
            {
                var (controlItems, scopedControlIds) = await BuildControlItems(standardId, evaluationId, userId, scopeToUser);
                var (requirementItems, scopedRequirementIds) = await BuildRequirementItems(standardId, evaluationId, userId, scopeToUser);

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

                var openBreachesCount = await _databaseService.Breach
                    .Where(b => (b.isDeleted == null || b.isDeleted == false) && b.evaluationId == evaluationId
                        && ((b.type == "2" && scopedControlIds.Contains(b.controlId))
                            || (b.type == "1" && scopedRequirementIds.Contains(b.requirementId))))
                    .CountAsync();

                return new GetGapDashboardDto
                {
                    totalItems = allItems.Count,
                    evaluatedItems = evaluatedItems.Count,
                    compliantItems = compliantItems.Count,
                    compliancePercentage = pct,
                    openBreachesCount = openBreachesCount,
                    maturityCounts = maturityCounts,
                    themes = themes,
                    pendingItems = pendingItems,
                };
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

        private record ItemState(string tipo, int itemId, string code, string name, string theme, string estado);

        private async Task<(List<ItemState> items, List<int> controlIds)> BuildControlItems(
            int standardId, int evaluationId, int userId, bool scopeToUser)
        {
            List<int>? assignedGroupIds = null;
            if (scopeToUser)
            {
                assignedGroupIds = await _databaseService.UserControlGroup
                    .Where(x => (x.isDeleted == null || x.isDeleted == false) && x.userId == userId && x.standardId == standardId)
                    .Select(x => x.controlGroupId)
                    .ToListAsync();
            }

            var groups = await _databaseService.ControlGroup
                .Where(g => (g.isDeleted == null || g.isDeleted == false) && g.standardId == standardId
                    && (assignedGroupIds == null || assignedGroupIds.Contains(g.controlGroupId)))
                .Select(g => new { g.controlGroupId, g.name, g.number })
                .ToListAsync();
            var groupById = groups.ToDictionary(g => g.controlGroupId);

            var controls = await _databaseService.Control
                .Where(c => (c.isDeleted == null || c.isDeleted == false) && c.standardId == standardId
                    && groupById.Keys.Contains(c.controlGroupId))
                .Select(c => new { c.controlId, c.controlGroupId, c.number, c.name })
                .ToListAsync();

            // Join explícito (no ce.maturityLevel.name): esa navigation property no está
            // configurada en ControlEvaluationConfiguration (está comentada) y acceder a ella
            // por punto genera el mismo error de shadow FK que ya se vio en User/Breach/ActionPlan.
            var controlEvaluations = await (
                from ce in _databaseService.ControlEvaluation
                join ml in _databaseService.MaturityLevel on ce.maturityLevel equals ml
                where (ce.isDeleted == null || ce.isDeleted == false) && ce.evaluationId == evaluationId
                select new { ce.controlId, maturityName = ml.name }
            ).ToListAsync();
            var maturityByControlId = controlEvaluations
                .GroupBy(ce => ce.controlId)
                .ToDictionary(g => g.Key, g => g.First().maturityName);

            var items = controls.Select(c =>
            {
                var group = groupById[c.controlGroupId];
                return new ItemState(
                    tipo: "control",
                    itemId: c.controlId,
                    code: $"{group.number}.{c.number}",
                    name: c.name,
                    theme: group.name,
                    estado: maturityByControlId.GetValueOrDefault(c.controlId) ?? PENDIENTE);
            }).ToList();

            return (items, controls.Select(c => c.controlId).ToList());
        }

        private async Task<(List<ItemState> items, List<int> requirementIds)> BuildRequirementItems(
            int standardId, int evaluationId, int userId, bool scopeToUser)
        {
            var allRequirements = await _databaseService.Requirement
                .Where(r => (r.isDeleted == null || r.isDeleted == false) && r.standardId == standardId)
                .Select(r => new { r.requirementId, r.parentId, r.level, r.isEvaluable, r.numeration, r.name })
                .ToListAsync();
            var byId = allRequirements.ToDictionary(r => r.requirementId);

            List<int>? assignedFamilyIds = null;
            if (scopeToUser)
            {
                assignedFamilyIds = await _databaseService.UserRequirementFamily
                    .Where(x => (x.isDeleted == null || x.isDeleted == false) && x.userId == userId && x.standardId == standardId)
                    .Select(x => x.requirementId)
                    .ToListAsync();
            }

            int? TopLevelAncestorId(int requirementId)
            {
                if (!byId.TryGetValue(requirementId, out var current)) return null;
                var guard = 0;
                while (current.level != 1 && guard++ < 20)
                {
                    if (!byId.TryGetValue(current.parentId, out var parent)) return null;
                    current = parent;
                }
                return current.level == 1 ? current.requirementId : null;
            }

            var evaluableRequirements = allRequirements.Where(r => r.isEvaluable).ToList();
            if (assignedFamilyIds != null)
            {
                evaluableRequirements = evaluableRequirements
                    .Where(r => assignedFamilyIds.Contains(TopLevelAncestorId(r.requirementId) ?? -1))
                    .ToList();
            }
            var scopedIds = evaluableRequirements.Select(r => r.requirementId).ToList();

            var requirementEvaluations = await (
                from re in _databaseService.RequirementEvaluation
                join ml in _databaseService.MaturityLevel on re.maturityLevel equals ml
                where (re.isDeleted == null || re.isDeleted == false) && re.evaluationId == evaluationId
                    && scopedIds.Contains(re.requirementId)
                select new { re.requirementId, maturityName = ml.name }
            ).ToListAsync();
            var maturityByRequirementId = requirementEvaluations
                .GroupBy(re => re.requirementId)
                .ToDictionary(g => g.Key, g => g.First().maturityName);

            var items = evaluableRequirements.Select(r => new ItemState(
                tipo: "requisito",
                itemId: r.requirementId,
                code: r.numeration.ToString(),
                name: r.name,
                theme: "Cláusulas",
                estado: maturityByRequirementId.GetValueOrDefault(r.requirementId) ?? PENDIENTE
            )).ToList();

            return (items, scopedIds);
        }
    }
}
