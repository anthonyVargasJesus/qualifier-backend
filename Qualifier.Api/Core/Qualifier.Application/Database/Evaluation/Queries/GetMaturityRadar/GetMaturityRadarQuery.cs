using Microsoft.EntityFrameworkCore;
using Qualifier.Application.Database.GapDashboard;
using Qualifier.Common.Application.Service;

namespace Qualifier.Application.Database.Evaluation.Queries.GetMaturityRadar
{
    // Radar de madurez por dominio del Anexo A: en vez de un solo % de controles agregado
    // (como ya muestran el dashboard de Inicio y Cumplimiento Histórico), desglosa el
    // cumplimiento dominio por dominio (A.5, A.6, A.7, A.8...) para que se vea de un vistazo
    // cuál domina arrastra el promedio — la pregunta que un solo número agregado no responde.
    // Reutiliza GapItemsBuilder.BuildControlItems (misma fuente que Cumplimiento Histórico y
    // el dashboard de Inicio) para no reimplementar la resolución de jerarquía/grupo de
    // controles ni el cálculo de qué cuenta como "evaluado".
    internal class GetMaturityRadarQuery : IGetMaturityRadarQuery
    {
        private const decimal MAX_VALUE = 5.0m;

        private readonly IDatabaseService _databaseService;
        private readonly GapItemsBuilder _itemsBuilder;

        public GetMaturityRadarQuery(IDatabaseService databaseService, GapItemsBuilder itemsBuilder)
        {
            _databaseService = databaseService;
            _itemsBuilder = itemsBuilder;
        }

        public async Task<Object> Execute(int companyId, int? standardId = null)
        {
            try
            {
                // Mismo criterio de resolución "sin standardId explícito, el de la evaluación
                // actual" que ya usa GetComplianceEvolutionQuery.
                int? resolvedStandardId = standardId;
                if (resolvedStandardId == null)
                {
                    resolvedStandardId = await (from eval in _databaseService.Evaluation
                                                 where (eval.isDeleted == null || eval.isDeleted == false)
                                                       && eval.companyId == companyId && eval.isCurrent
                                                 select (int?)eval.standardId).FirstOrDefaultAsync();
                }

                var dto = new GetMaturityRadarDto { standardId = resolvedStandardId };
                if (resolvedStandardId == null)
                    return dto;

                dto.standardName = await (from s in _databaseService.Standard
                                           where s.standardId == resolvedStandardId
                                           select s.name).FirstOrDefaultAsync();

                var evaluations = await (from eval in _databaseService.Evaluation
                                          where (eval.isDeleted == null || eval.isDeleted == false)
                                                && eval.companyId == companyId && eval.standardId == resolvedStandardId
                                          orderby eval.startDate
                                          select new { eval.evaluationId, eval.description, eval.isCurrent }).ToListAsync();

                if (!evaluations.Any())
                    return dto;

                // Mismo fallback que GetComplianceEvolutionQuery: si ninguna evaluación de ESTA
                // norma está marcada isCurrent (esa bandera es global), se toma la más reciente.
                var currentEval = evaluations.FirstOrDefault(e => e.isCurrent) ?? evaluations[^1];
                var currentIndex = evaluations.FindIndex(e => e.evaluationId == currentEval.evaluationId);
                var previousEval = currentIndex > 0 ? evaluations[currentIndex - 1] : null;

                dto.currentEvaluationId = currentEval.evaluationId;
                dto.currentEvaluationDescription = currentEval.description;
                dto.previousEvaluationId = previousEval?.evaluationId;
                dto.previousEvaluationDescription = previousEval?.description;

                var (currentItems, _) = await _itemsBuilder.BuildControlItems(resolvedStandardId.Value, currentEval.evaluationId, userId: 0, scopeToUser: false);

                List<GapItemsBuilder.ItemState>? previousItems = null;
                if (previousEval != null)
                    (previousItems, _) = await _itemsBuilder.BuildControlItems(resolvedStandardId.Value, previousEval.evaluationId, userId: 0, scopeToUser: false);

                var domainGroups = currentItems
                    .GroupBy(i => new { i.theme, i.groupNumber })
                    .OrderBy(g => g.Key.groupNumber ?? 0)
                    .ToList();

                foreach (var g in domainGroups)
                {
                    var evaluatedCurrent = g.Where(i => i.value.HasValue).ToList();
                    decimal? currentRate = evaluatedCurrent.Any()
                        ? Math.Round(evaluatedCurrent.Average(i => i.value!.Value) / MAX_VALUE * 100, 1)
                        : (decimal?)null;

                    decimal? previousRate = null;
                    if (previousItems != null)
                    {
                        var previousEvaluated = previousItems.Where(i => i.theme == g.Key.theme && i.value.HasValue).ToList();
                        if (previousEvaluated.Any())
                            previousRate = Math.Round(previousEvaluated.Average(i => i.value!.Value) / MAX_VALUE * 100, 1);
                    }

                    dto.domains.Add(new GetMaturityRadarDomainDto
                    {
                        code = g.Key.groupNumber?.ToString("0.##", System.Globalization.CultureInfo.InvariantCulture) ?? "",
                        name = g.Key.theme,
                        currentRate = currentRate,
                        previousRate = previousRate,
                        delta = currentRate.HasValue && previousRate.HasValue ? Math.Round(currentRate.Value - previousRate.Value, 1) : (decimal?)null,
                        totalControls = g.Count(),
                        evaluatedControls = evaluatedCurrent.Count,
                    });
                }

                var ratedCurrent = dto.domains.Where(d => d.currentRate.HasValue).ToList();
                if (ratedCurrent.Any())
                {
                    dto.overallCurrentRate = Math.Round(ratedCurrent.Average(d => d.currentRate!.Value), 1);
                    dto.weakestDomain = ratedCurrent.OrderBy(d => d.currentRate).First();
                    dto.strongestDomain = ratedCurrent.OrderByDescending(d => d.currentRate).First();
                }

                var ratedPrevious = dto.domains.Where(d => d.previousRate.HasValue).ToList();
                if (ratedPrevious.Any())
                    dto.overallPreviousRate = Math.Round(ratedPrevious.Average(d => d.previousRate!.Value), 1);

                return dto;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }
    }
}
