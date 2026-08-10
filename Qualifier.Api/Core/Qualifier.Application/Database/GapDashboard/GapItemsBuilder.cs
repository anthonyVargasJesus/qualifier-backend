using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Service;

namespace Qualifier.Application.Database.GapDashboard
{
    // Construye la lista "aplanada" de ítems evaluables (controles + requisitos) de un
    // estándar/evaluación, con su estado de madurez y si tienen evidencia adjunta. Compartido
    // entre GetGapDashboardQuery (stats agregadas del dashboard de Inicio) y
    // GetMissingEvidenceReportQuery (el detalle de qué ítems faltan) — un solo lugar para esta
    // lógica de scoping/jerarquía evita que ambos endpoints puedan divergir en qué cuenta como
    // "evaluado" o "con evidencia".
    public class GapItemsBuilder
    {
        public const string PENDIENTE = "Pendiente";
        public const string NO_APLICA = "No aplica";
        public const string CUMPLE = "Cumple";

        // Para ordenar ItemState.code ("4", "4.1", "10", "10.1.1"...) numéricamente en vez
        // de alfabéticamente. Un ".OrderBy(i => i.code)" plano pone "10" antes que "4"
        // (compara char a char: '1' < '4'), así que la cláusula 10 completa aparecía antes
        // que la 4 — mismo problema en las 4 queries que listan/ordenan ItemState
        // (GetGapItemsQuery, GetGapDashboardQuery, GetMissingEvidenceReportQuery,
        // GetSoaReportQuery). Rellena cada segmento numérico a 4 dígitos ("0004", "0010") para
        // que la comparación de strings resultante coincida con el orden numérico real, y deja
        // pasar tal cual cualquier segmento no numérico (no debería haber, pero por seguridad).
        public static string NaturalSortKey(string code) =>
            string.Join('.', code.Split('.').Select(s => int.TryParse(s, out var n) ? n.ToString("D4") : s));

        private readonly IDatabaseService _databaseService;

        public GapItemsBuilder(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        // evaluationItemId/maturityLevelId/value/improvementActions/responsibleId: agregados
        // para GetGapItemsQuery (lista paginada de gap-home), que necesita lo suficiente para
        // renderizar y guardar la tarjeta editable sin volver a construir el árbol completo de
        // GetRequirementEvaluationByProcessQuery. GetGapDashboardQuery y
        // GetMissingEvidenceReportQuery no los usan; un record con más campos no les afecta.
        // groupNumber: solo para items tipo "control" (null en requisitos) — el número real de
        // ControlGroup (puede ser "6.1" en normas con sub-grupos anidados, ej. NTP 42001), para
        // que GetGapSummaryQuery pueda ordenar los temas sin tener que reparsear "code".
        public record ItemState(string tipo, int itemId, string code, string name, string theme, string estado, bool hasEvidence, string? justification,
            long? evaluationItemId = null, int? maturityLevelId = null, decimal? value = null, string? improvementActions = null, int? responsibleId = null,
            decimal? groupNumber = null);

        public async Task<(List<ItemState> items, List<int> controlIds)> BuildControlItems(
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
            // orderby controlEvaluationId descending: no debería haber más de una fila por
            // (controlId, evaluationId), pero no hay constraint único que lo garantice — si
            // llegan a existir duplicados, los 3 GroupBy+First() de abajo se quedan
            // consistentemente con la más reciente (mismo criterio que
            // ControlEntity.setEvaluations/GetActionPlansByUserIdQuery).
            var controlEvaluations = await (
                from ce in _databaseService.ControlEvaluation
                join ml in _databaseService.MaturityLevel on ce.maturityLevel equals ml
                where (ce.isDeleted == null || ce.isDeleted == false) && ce.evaluationId == evaluationId
                orderby ce.controlEvaluationId descending
                select new { ce.controlId, ce.controlEvaluationId, maturityName = ml.name, ce.justification,
                    ce.maturityLevelId, ce.value, ce.improvementActions, ce.responsibleId }
            ).ToListAsync();
            var maturityByControlId = controlEvaluations
                .GroupBy(ce => ce.controlId)
                .ToDictionary(g => g.Key, g => g.First().maturityName);
            var controlEvaluationIdByControlId = controlEvaluations
                .GroupBy(ce => ce.controlId)
                .ToDictionary(g => g.Key, g => g.First().controlEvaluationId);
            var justificationByControlId = controlEvaluations
                .GroupBy(ce => ce.controlId)
                .ToDictionary(g => g.Key, g => g.First().justification);
            var maturityLevelIdByControlId = controlEvaluations
                .GroupBy(ce => ce.controlId)
                .ToDictionary(g => g.Key, g => (int?)g.First().maturityLevelId);
            var valueByControlId = controlEvaluations
                .GroupBy(ce => ce.controlId)
                .ToDictionary(g => g.Key, g => (decimal?)g.First().value);
            var improvementActionsByControlId = controlEvaluations
                .GroupBy(ce => ce.controlId)
                .ToDictionary(g => g.Key, g => g.First().improvementActions);
            var responsibleIdByControlId = controlEvaluations
                .GroupBy(ce => ce.controlId)
                .ToDictionary(g => g.Key, g => (int?)g.First().responsibleId);

            // Ítems (evaluados) que tienen al menos una evidencia adjunta — indicador de
            // "% con evidencia" del dashboard de Inicio. Una sola consulta agrupada (no N+1)
            // sobre los controlEvaluationId ya resueltos arriba.
            var controlEvaluationIds = controlEvaluations.Select(ce => ce.controlEvaluationId).ToList();
            var controlEvaluationIdsWithEvidence = (await _databaseService.ReferenceDocumentation
                .Where(rd => (rd.isDeleted == null || rd.isDeleted == false)
                    && rd.controlEvaluationId != null && controlEvaluationIds.Contains(rd.controlEvaluationId.Value))
                .Select(rd => rd.controlEvaluationId!.Value)
                .Distinct()
                .ToListAsync()).ToHashSet();

            var items = controls.Select(c =>
            {
                var group = groupById[c.controlGroupId];
                var hasEvaluation = controlEvaluationIdByControlId.TryGetValue(c.controlId, out var controlEvaluationId);
                return new ItemState(
                    tipo: "control",
                    itemId: c.controlId,
                    code: $"{group.number.ToString("0.##", System.Globalization.CultureInfo.InvariantCulture)}.{c.number}",
                    name: c.name,
                    theme: group.name,
                    estado: maturityByControlId.GetValueOrDefault(c.controlId) ?? PENDIENTE,
                    hasEvidence: hasEvaluation && controlEvaluationIdsWithEvidence.Contains(controlEvaluationId),
                    justification: justificationByControlId.GetValueOrDefault(c.controlId),
                    evaluationItemId: hasEvaluation ? controlEvaluationId : null,
                    maturityLevelId: maturityLevelIdByControlId.GetValueOrDefault(c.controlId),
                    value: valueByControlId.GetValueOrDefault(c.controlId),
                    improvementActions: improvementActionsByControlId.GetValueOrDefault(c.controlId),
                    responsibleId: responsibleIdByControlId.GetValueOrDefault(c.controlId),
                    groupNumber: group.number);
            }).ToList();

            return (items, controls.Select(c => c.controlId).ToList());
        }

        public async Task<(List<ItemState> items, List<int> requirementIds)> BuildRequirementItems(
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

            // Mismo cálculo que OverrideNumerationToShowWithHierarchy en
            // GetRequirementEvaluationByProcessQuery (el "código" real, tipo "4.1", "6.1.2"),
            // pero sin armar el árbol completo de StandardEntity — acá alcanza con subir por
            // parentId y concatenar. Memoizado porque varios requisitos comparten ancestros.
            var numerationToShowCache = new Dictionary<int, string>();
            string ResolveNumerationToShow(int requirementId)
            {
                if (numerationToShowCache.TryGetValue(requirementId, out var cached)) return cached;
                if (!byId.TryGetValue(requirementId, out var current)) return "";
                var result = current.level <= 1 || !byId.ContainsKey(current.parentId)
                    ? current.numeration.ToString()
                    : $"{ResolveNumerationToShow(current.parentId)}.{current.numeration}";
                numerationToShowCache[requirementId] = result;
                return result;
            }

            var evaluableRequirements = allRequirements.Where(r => r.isEvaluable).ToList();
            if (assignedFamilyIds != null)
            {
                evaluableRequirements = evaluableRequirements
                    .Where(r => assignedFamilyIds.Contains(TopLevelAncestorId(r.requirementId) ?? -1))
                    .ToList();
            }
            var scopedIds = evaluableRequirements.Select(r => r.requirementId).ToList();

            // orderby requirementEvaluationId descending: mismo motivo que en
            // BuildControlItems — deja los 3 GroupBy+First() de abajo consistentemente con la
            // fila más reciente si llegan a existir duplicados.
            var requirementEvaluations = await (
                from re in _databaseService.RequirementEvaluation
                join ml in _databaseService.MaturityLevel on re.maturityLevel equals ml
                where (re.isDeleted == null || re.isDeleted == false) && re.evaluationId == evaluationId
                    && scopedIds.Contains(re.requirementId)
                orderby re.requirementEvaluationId descending
                select new { re.requirementId, re.requirementEvaluationId, maturityName = ml.name, re.justification,
                    re.maturityLevelId, re.value, re.improvementActions, re.responsibleId }
            ).ToListAsync();
            var maturityByRequirementId = requirementEvaluations
                .GroupBy(re => re.requirementId)
                .ToDictionary(g => g.Key, g => g.First().maturityName);
            var requirementEvaluationIdByRequirementId = requirementEvaluations
                .GroupBy(re => re.requirementId)
                .ToDictionary(g => g.Key, g => g.First().requirementEvaluationId);
            var justificationByRequirementId = requirementEvaluations
                .GroupBy(re => re.requirementId)
                .ToDictionary(g => g.Key, g => g.First().justification);
            var maturityLevelIdByRequirementId = requirementEvaluations
                .GroupBy(re => re.requirementId)
                .ToDictionary(g => g.Key, g => (int?)g.First().maturityLevelId);
            var valueByRequirementId = requirementEvaluations
                .GroupBy(re => re.requirementId)
                .ToDictionary(g => g.Key, g => (decimal?)g.First().value);
            var improvementActionsByRequirementId = requirementEvaluations
                .GroupBy(re => re.requirementId)
                .ToDictionary(g => g.Key, g => g.First().improvementActions);
            var responsibleIdByRequirementId = requirementEvaluations
                .GroupBy(re => re.requirementId)
                .ToDictionary(g => g.Key, g => (int?)g.First().responsibleId);

            // Mismo criterio que en BuildControlItems: una sola consulta agrupada para saber
            // qué evaluaciones ya tienen evidencia adjunta.
            var requirementEvaluationIds = requirementEvaluations.Select(re => re.requirementEvaluationId).ToList();
            var requirementEvaluationIdsWithEvidence = (await _databaseService.ReferenceDocumentation
                .Where(rd => (rd.isDeleted == null || rd.isDeleted == false)
                    && rd.requirementEvaluationId != null && requirementEvaluationIds.Contains(rd.requirementEvaluationId.Value))
                .Select(rd => rd.requirementEvaluationId!.Value)
                .Distinct()
                .ToListAsync()).ToHashSet();

            var items = evaluableRequirements.Select(r =>
            {
                var hasEvaluation = requirementEvaluationIdByRequirementId.TryGetValue(r.requirementId, out var requirementEvaluationId);
                return new ItemState(
                    tipo: "requisito",
                    itemId: r.requirementId,
                    code: ResolveNumerationToShow(r.requirementId),
                    name: r.name,
                    theme: "Cláusulas",
                    estado: maturityByRequirementId.GetValueOrDefault(r.requirementId) ?? PENDIENTE,
                    hasEvidence: hasEvaluation && requirementEvaluationIdsWithEvidence.Contains(requirementEvaluationId),
                    justification: justificationByRequirementId.GetValueOrDefault(r.requirementId),
                    evaluationItemId: hasEvaluation ? requirementEvaluationId : null,
                    maturityLevelId: maturityLevelIdByRequirementId.GetValueOrDefault(r.requirementId),
                    value: valueByRequirementId.GetValueOrDefault(r.requirementId),
                    improvementActions: improvementActionsByRequirementId.GetValueOrDefault(r.requirementId),
                    responsibleId: responsibleIdByRequirementId.GetValueOrDefault(r.requirementId));
            }).ToList();

            return (items, scopedIds);
        }
    }
}
