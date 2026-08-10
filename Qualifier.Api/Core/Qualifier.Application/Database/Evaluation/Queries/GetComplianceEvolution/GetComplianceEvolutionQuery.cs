using Microsoft.EntityFrameworkCore;
using Qualifier.Application.Database.GapDashboard;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.Evaluation.Queries.GetComplianceEvolution
{
    internal class GetComplianceEvolutionQuery : IGetComplianceEvolutionQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly GapItemsBuilder _itemsBuilder;

        public GetComplianceEvolutionQuery(IDatabaseService databaseService, GapItemsBuilder itemsBuilder)
        {
            _databaseService = databaseService;
            _itemsBuilder = itemsBuilder;
        }

        public async Task<Object> Execute(int companyId, int? standardId = null)
        {
            try
            {
                const decimal MAX_VALUE = 5.0m;

                // Sin standardId explícito, se resuelve al de la evaluación "actual" — mismo
                // criterio implícito que ya usan las demás pantallas de evaluación para saber
                // "de qué norma estamos hablando" sin pedírselo al usuario. Antes esta query
                // no filtraba por norma en absoluto: mezclaba en una sola línea de tiempo las
                // evaluaciones de TODAS las normas de la empresa (viable mientras solo existía
                // una norma cargada; con más de una, el gráfico y los deltas quedaban
                // mezclados/rotos — más aún con fechas de inicio empatadas entre normas, ver
                // el "actual"/"anterior" de abajo).
                int? resolvedStandardId = standardId;
                if (resolvedStandardId == null)
                {
                    var currentStandardId = await (from eval in _databaseService.Evaluation
                                                    where (eval.isDeleted == null || eval.isDeleted == false)
                                                          && eval.companyId == companyId && eval.isCurrent
                                                    select (int?)eval.standardId).FirstOrDefaultAsync();
                    resolvedStandardId = currentStandardId;
                }

                var evaluations = await (from eval in _databaseService.Evaluation
                                         where (eval.isDeleted == null || eval.isDeleted == false)
                                               && eval.companyId == companyId
                                               && (resolvedStandardId == null || eval.standardId == resolvedStandardId)
                                         orderby eval.startDate
                                         select new EvaluationEntity
                                         {
                                             evaluationId = eval.evaluationId,
                                             description = eval.description,
                                             startDate = eval.startDate,
                                             endDate = eval.endDate,
                                             isCurrent = eval.isCurrent,
                                         }).ToListAsync();

                string? standardName = resolvedStandardId != null
                    ? await (from s in _databaseService.Standard
                             where s.standardId == resolvedStandardId
                             select s.name).FirstOrDefaultAsync()
                    : null;

                if (!evaluations.Any())
                    return new GetComplianceEvolutionDto { totalEvaluations = 0, standardId = resolvedStandardId, standardName = standardName };

                // Filtradas por evaluationId (ya acotado a la norma resuelta arriba), no por
                // el standardId propio de cada fila de requirement/control evaluation — evita
                // depender de que esa columna redundante esté siempre poblada.
                var evaluationIds = evaluations.Select(e => e.evaluationId).ToList();

                // Filas crudas (no promedio por evaluación todavía): hace falta el detalle por
                // ítem para "arrastrar" el último valor conocido — ver el porqué abajo.
                var reqRows = await (from re in _databaseService.RequirementEvaluation
                                     where re.companyId == companyId && evaluationIds.Contains(re.evaluationId)
                                     select new { re.evaluationId, re.requirementId, re.value }).ToListAsync();

                var ctrlRows = await (from ce in _databaseService.ControlEvaluation
                                      where ce.companyId == companyId && evaluationIds.Contains(ce.evaluationId)
                                      select new { ce.evaluationId, ce.controlId, ce.value }).ToListAsync();

                // "Arrastre" del último valor conocido por ítem: algunas evaluaciones (ej. una
                // reevaluación parcial que solo vuelve a tocar los ítems que estaban en brecha)
                // no traen fila para TODOS los ítems del estándar — un promedio calculado solo
                // sobre lo presente en esa evaluación puntual queda sesgado hacia abajo (mezcla
                // solo los peores ítems, sin contar los que ya estaban bien y no se re-tocaron).
                // Recorriendo las evaluaciones en orden cronológico y actualizando un diccionario
                // "valor más reciente por ítem", el promedio en cada punto refleja el estado
                // acumulado real a esa fecha (lo tocado en esta evaluación + lo heredado de la
                // anterior), igual a como se leería el estado real del SGSI en ese momento.
                var reqLastKnown = new Dictionary<int, decimal>();
                var ctrlLastKnown = new Dictionary<int, decimal>();

                // Copia del estado "a la fecha de esta evaluación" en cada paso — para poder
                // comparar dos evaluaciones cualquiera más abajo (actual vs. anterior) sin tener
                // que volver a recorrer reqRows/ctrlRows desde cero.
                var reqSnapshotsByEvalId = new Dictionary<int, Dictionary<int, decimal>>();
                var ctrlSnapshotsByEvalId = new Dictionary<int, Dictionary<int, decimal>>();

                var items = evaluations.Select(eval =>
                {
                    foreach (var row in reqRows.Where(r => r.evaluationId == eval.evaluationId))
                        reqLastKnown[row.requirementId] = row.value;
                    foreach (var row in ctrlRows.Where(r => r.evaluationId == eval.evaluationId))
                        ctrlLastKnown[row.controlId] = row.value;

                    reqSnapshotsByEvalId[eval.evaluationId] = new Dictionary<int, decimal>(reqLastKnown);
                    ctrlSnapshotsByEvalId[eval.evaluationId] = new Dictionary<int, decimal>(ctrlLastKnown);

                    decimal? reqRate = reqLastKnown.Count > 0
                        ? Math.Round(reqLastKnown.Values.Average() / MAX_VALUE * 100, 1) : (decimal?)null;
                    decimal? ctrlRate = ctrlLastKnown.Count > 0
                        ? Math.Round(ctrlLastKnown.Values.Average() / MAX_VALUE * 100, 1) : (decimal?)null;
                    decimal? overall = reqRate.HasValue && ctrlRate.HasValue
                        ? Math.Round((reqRate.Value + ctrlRate.Value) / 2, 1)
                        : reqRate ?? ctrlRate;

                    return new GetComplianceEvolutionItemDto
                    {
                        evaluationId = eval.evaluationId,
                        description = eval.description,
                        startDate = eval.startDate,
                        endDate = eval.endDate,
                        isCurrent = eval.isCurrent,
                        requirementsRate = reqRate,
                        controlsRate = ctrlRate,
                        overallRate = overall,
                        requirementsCount = reqLastKnown.Count,
                        controlsCount = ctrlLastKnown.Count,
                    };
                }).ToList();

                var reqSeries = items
                    .Where(i => i.requirementsRate.HasValue)
                    .Select(i => new GetComplianceEvolutionSeriesDto { name = GetShortLabel(i), value = i.requirementsRate!.Value })
                    .ToList();

                var ctrlSeries = items
                    .Where(i => i.controlsRate.HasValue)
                    .Select(i => new GetComplianceEvolutionSeriesDto { name = GetShortLabel(i), value = i.controlsRate!.Value })
                    .ToList();

                var overallSeries = items
                    .Where(i => i.overallRate.HasValue)
                    .Select(i => new GetComplianceEvolutionSeriesDto { name = GetShortLabel(i), value = i.overallRate!.Value })
                    .ToList();

                var chart = new List<GetComplianceEvolutionLineDto>();
                if (overallSeries.Any()) chart.Add(new GetComplianceEvolutionLineDto { name = "General", series = overallSeries });
                if (reqSeries.Any()) chart.Add(new GetComplianceEvolutionLineDto { name = "Requisitos", series = reqSeries });
                if (ctrlSeries.Any()) chart.Add(new GetComplianceEvolutionLineDto { name = "Controles", series = ctrlSeries });

                // isCurrent es global (una sola fila en todo el sistema, ver arriba) — para una
                // norma que hoy no la tiene (ej. NTP 42001, a propósito: no debe pisar la
                // evaluación actual de ISO 27001), sin este fallback las tarjetas "(actual)"
                // quedarían vacías aunque la norma sí tenga historial. Dentro de ESTE reporte
                // (ya acotado a una sola norma), lo esperable es que "actual" sea la evaluación
                // más reciente de esa norma cuando ninguna está marcada isCurrent globalmente.
                var currentItem = items.FirstOrDefault(i => i.isCurrent) ?? items.LastOrDefault();
                var currentIndex = currentItem != null ? items.IndexOf(currentItem) : -1;
                var previousItem = currentIndex > 0 ? items[currentIndex - 1] : null;

                // "¿En qué mejoramos/empeoramos?" — la pregunta obvia después de ver que el %
                // subió. Compara, ítem por ítem, el nivel de madurez en la evaluación actual
                // contra la anterior (mismo par que ya usan las tarjetas de delta arriba) y
                // lista los que cambiaron. Solo ítems presentes en AMBAS instantáneas — uno que
                // recién se evaluó por primera vez no es una "mejora", es una primera medición.
                var changes = new List<GetComplianceEvolutionChangeDto>();
                int improvedCount = 0, worsenedCount = 0;

                if (currentItem != null && previousItem != null && resolvedStandardId != null)
                {
                    var levelNames = await _databaseService.MaturityLevel
                        .Select(m => new { m.value, m.name }).ToListAsync();
                    string LevelName(decimal v) => levelNames.FirstOrDefault(l => l.value == v)?.name ?? v.ToString();

                    var currentReqSnap = reqSnapshotsByEvalId.GetValueOrDefault(currentItem.evaluationId) ?? new();
                    var previousReqSnap = reqSnapshotsByEvalId.GetValueOrDefault(previousItem.evaluationId) ?? new();
                    var currentCtrlSnap = ctrlSnapshotsByEvalId.GetValueOrDefault(currentItem.evaluationId) ?? new();
                    var previousCtrlSnap = ctrlSnapshotsByEvalId.GetValueOrDefault(previousItem.evaluationId) ?? new();

                    // Catálogo de código/nombre por ítem — se reutiliza GapItemsBuilder (misma
                    // fuente que ya usa "Análisis del GAP") en vez de reimplementar la
                    // resolución de jerarquía/numeración de requisitos y grupo.control de
                    // controles. El evaluationId que se le pasa solo importa para resolver el
                    // "estado" (que acá se ignora); el código/nombre es del catálogo, no cambia
                    // entre evaluaciones.
                    var (reqCatalog, _) = await _itemsBuilder.BuildRequirementItems(resolvedStandardId.Value, currentItem.evaluationId, userId: 0, scopeToUser: false);
                    var (ctrlCatalog, _) = await _itemsBuilder.BuildControlItems(resolvedStandardId.Value, currentItem.evaluationId, userId: 0, scopeToUser: false);
                    var reqCatalogById = reqCatalog.ToDictionary(r => r.itemId);
                    var ctrlCatalogById = ctrlCatalog.ToDictionary(c => c.itemId);

                    foreach (var id in currentReqSnap.Keys.Union(previousReqSnap.Keys))
                    {
                        if (!currentReqSnap.TryGetValue(id, out var curVal) || !previousReqSnap.TryGetValue(id, out var prevVal)) continue;
                        if (curVal == prevVal || !reqCatalogById.TryGetValue(id, out var cat)) continue;
                        changes.Add(new GetComplianceEvolutionChangeDto
                        {
                            tipo = "requisito", code = cat.code, name = cat.name,
                            previousLevel = LevelName(prevVal), currentLevel = LevelName(curVal),
                            improved = curVal > prevVal,
                        });
                    }
                    foreach (var id in currentCtrlSnap.Keys.Union(previousCtrlSnap.Keys))
                    {
                        if (!currentCtrlSnap.TryGetValue(id, out var curVal) || !previousCtrlSnap.TryGetValue(id, out var prevVal)) continue;
                        if (curVal == prevVal || !ctrlCatalogById.TryGetValue(id, out var cat)) continue;
                        changes.Add(new GetComplianceEvolutionChangeDto
                        {
                            tipo = "control", code = cat.code, name = cat.name,
                            previousLevel = LevelName(prevVal), currentLevel = LevelName(curVal),
                            improved = curVal > prevVal,
                        });
                    }

                    changes = changes.OrderByDescending(c => c.improved).ThenBy(c => GapItemsBuilder.NaturalSortKey(c.code)).ToList();
                    improvedCount = changes.Count(c => c.improved);
                    worsenedCount = changes.Count(c => !c.improved);
                }

                // Composición por nivel de madurez de la evaluación ACTUAL (no un delta contra
                // la anterior, así que no depende de que exista previousItem) — misma
                // instantánea "a la fecha" que ya se calculó arriba para currentItem.
                var reqComposition = new List<GetComplianceEvolutionCompositionDto>();
                var ctrlComposition = new List<GetComplianceEvolutionCompositionDto>();
                if (currentItem != null)
                {
                    var levels = await _databaseService.MaturityLevel
                        .OrderByDescending(m => m.value)
                        .Select(m => new { m.value, m.name, m.color }).ToListAsync();

                    List<GetComplianceEvolutionCompositionDto> BuildComposition(Dictionary<int, decimal> snapshot)
                    {
                        var total = snapshot.Count;
                        return levels
                            .Select(l => new { l.name, l.color, count = snapshot.Values.Count(v => v == l.value) })
                            .Where(x => x.count > 0)
                            .Select(x => new GetComplianceEvolutionCompositionDto
                            {
                                name = x.name ?? string.Empty,
                                color = x.color,
                                count = x.count,
                                percentage = total > 0 ? Math.Round((decimal)x.count / total * 100, 1) : 0,
                            }).ToList();
                    }

                    reqComposition = BuildComposition(reqSnapshotsByEvalId.GetValueOrDefault(currentItem.evaluationId) ?? new());
                    ctrlComposition = BuildComposition(ctrlSnapshotsByEvalId.GetValueOrDefault(currentItem.evaluationId) ?? new());
                }

                return new GetComplianceEvolutionDto
                {
                    standardId = resolvedStandardId,
                    standardName = standardName,
                    totalEvaluations = items.Count,
                    currentRequirementsRate = currentItem?.requirementsRate,
                    currentControlsRate = currentItem?.controlsRate,
                    currentOverallRate = currentItem?.overallRate,
                    deltaRequirements = currentItem != null && previousItem != null
                        && currentItem.requirementsRate.HasValue && previousItem.requirementsRate.HasValue
                        ? Math.Round(currentItem.requirementsRate.Value - previousItem.requirementsRate.Value, 1)
                        : (decimal?)null,
                    deltaControls = currentItem != null && previousItem != null
                        && currentItem.controlsRate.HasValue && previousItem.controlsRate.HasValue
                        ? Math.Round(currentItem.controlsRate.Value - previousItem.controlsRate.Value, 1)
                        : (decimal?)null,
                    currentEvaluationDescription = currentItem?.description,
                    previousEvaluationDescription = previousItem?.description,
                    evaluations = items,
                    chart = chart,
                    improvedCount = improvedCount,
                    worsenedCount = worsenedCount,
                    changes = changes,
                    requirementsComposition = reqComposition,
                    controlsComposition = ctrlComposition,
                };
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

        private static string GetShortLabel(GetComplianceEvolutionItemDto item)
        {
            if (!string.IsNullOrEmpty(item.description))
                return item.description.Length > 18 ? item.description[..18] + "…" : item.description;
            return item.startDate.Year.ToString();
        }
    }
}
