using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Service;

namespace Qualifier.Application.Database.Breach.Queries.GetBreachesWithoutActionPlan
{
    // Reporte "Brechas sin plan de acción" para el dueño del requisito: brechas abiertas
    // reconocidas pero sin ninguna acción correctiva definida todavía. Endpoint propio — no
    // reutiliza /api/breach/severity-report (que además no filtra isDeleted en las brechas y
    // usa INNER JOIN contra Responsible, así que descarta en silencio las brechas sin
    // responsable asignado; acá se corrigen ambos casos con LEFT JOIN + "Sin asignar").
    public class GetBreachesWithoutActionPlanQuery : IGetBreachesWithoutActionPlanQuery
    {
        private readonly IDatabaseService _databaseService;

        public GetBreachesWithoutActionPlanQuery(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public async Task<Object> Execute(int companyId)
        {
            try
            {
                var currentEvaluation = await (
                    from eval in _databaseService.Evaluation
                    where (eval.isDeleted == null || eval.isDeleted == false)
                        && eval.companyId == companyId
                        && eval.isCurrent
                    select new { eval.evaluationId }
                ).FirstOrDefaultAsync();

                if (currentEvaluation == null)
                    return new GetBreachesWithoutActionPlanDto { hasCurrentEvaluation = false };

                var breaches = await (
                    from b in _databaseService.Breach
                    join severity in _databaseService.BreachSeverity on b.breachSeverityId equals severity.breachSeverityId
                    join responsible in _databaseService.Responsible on b.responsibleId equals responsible.responsibleId into responsibleJoin
                    from responsible in responsibleJoin.DefaultIfEmpty()
                    where (b.isDeleted == null || b.isDeleted == false)
                        && b.companyId == companyId
                        && b.evaluationId == currentEvaluation.evaluationId
                    select new
                    {
                        b.breachId,
                        b.title,
                        b.numerationToShow,
                        severityName = severity.name,
                        severityColor = severity.color,
                        severityValue = severity.value,
                        responsibleName = responsible == null ? "Sin asignar" : responsible.name,
                    }
                ).ToListAsync();

                // Conteo de planes por brecha en una sola consulta agrupada (no N+1) — solo
                // hace falta saber cuáles brechas tienen 0.
                var actionPlanCountByBreachId = await (
                    from ap in _databaseService.ActionPlan
                    where (ap.isDeleted == null || ap.isDeleted == false)
                        && ap.companyId == companyId
                        && ap.evaluationId == currentEvaluation.evaluationId
                    group ap by ap.breachId into g
                    select g.Key
                ).ToListAsync();
                var breachIdsWithPlan = actionPlanCountByBreachId.ToHashSet();

                var items = breaches
                    .Where(b => !breachIdsWithPlan.Contains(b.breachId))
                    .OrderByDescending(b => b.severityValue)
                    .Select(b => new GetBreachWithoutActionPlanItemDto
                    {
                        breachId = b.breachId,
                        title = b.title,
                        numerationToShow = b.numerationToShow,
                        severityName = b.severityName,
                        severityColor = b.severityColor,
                        responsibleName = b.responsibleName,
                    })
                    .ToList();

                return new GetBreachesWithoutActionPlanDto
                {
                    hasCurrentEvaluation = true,
                    evaluationId = currentEvaluation.evaluationId,
                    totalBreaches = breaches.Count,
                    withoutActionPlanCount = items.Count,
                    items = items,
                };
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }
    }
}
