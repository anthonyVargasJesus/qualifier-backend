using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Service;

namespace Qualifier.Application.Database.Breach.Queries.GetBreachAgingReport
{
    // Reporte "Antigüedad de brechas abiertas" para el dueño del requisito: cuánto tiempo
    // llevan sin resolverse, agrupadas en buckets. "Abierta" usa el mismo criterio que ya
    // muestra el dashboard de Inicio (GetGapDashboardQuery.openBreachesCount): cualquier
    // brecha no eliminada de la evaluación actual, sin filtrar por breachStatusId — para que
    // el número de acá no contradiga el "23 brechas abiertas" que ya ve en Inicio.
    internal class GetBreachAgingReportQuery : IGetBreachAgingReportQuery
    {
        private const int BUCKET_1_MAX_DAYS = 30;
        private const int BUCKET_2_MAX_DAYS = 60;

        private readonly IDatabaseService _databaseService;

        public GetBreachAgingReportQuery(IDatabaseService databaseService)
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
                    return new GetBreachAgingReportDto { hasCurrentEvaluation = false };

                var breaches = await (
                    from b in _databaseService.Breach
                    join severity in _databaseService.BreachSeverity on b.breachSeverityId equals severity.breachSeverityId
                    where (b.isDeleted == null || b.isDeleted == false)
                        && b.companyId == companyId
                        && b.evaluationId == currentEvaluation.evaluationId
                    select new
                    {
                        b.breachId,
                        b.title,
                        b.numerationToShow,
                        b.creationDate,
                        severityName = severity.name,
                        severityColor = severity.color,
                    }
                ).ToListAsync();

                var today = DateTime.UtcNow;
                var items = breaches
                    .Select(b => new GetBreachAgingItemDto
                    {
                        breachId = b.breachId,
                        title = b.title,
                        numerationToShow = b.numerationToShow,
                        daysOpen = b.creationDate.HasValue ? (int)(today - b.creationDate.Value).TotalDays : 0,
                        severityName = b.severityName,
                        severityColor = b.severityColor,
                    })
                    .OrderByDescending(i => i.daysOpen)
                    .ToList();

                var buckets = new List<GetBreachAgingBucketDto>
                {
                    new GetBreachAgingBucketDto
                    {
                        label = "0-30 días",
                        breaches = items.Where(i => i.daysOpen <= BUCKET_1_MAX_DAYS).ToList(),
                    },
                    new GetBreachAgingBucketDto
                    {
                        label = "31-60 días",
                        breaches = items.Where(i => i.daysOpen > BUCKET_1_MAX_DAYS && i.daysOpen <= BUCKET_2_MAX_DAYS).ToList(),
                    },
                    new GetBreachAgingBucketDto
                    {
                        label = "Más de 60 días",
                        breaches = items.Where(i => i.daysOpen > BUCKET_2_MAX_DAYS).ToList(),
                    },
                };
                foreach (var bucket in buckets)
                    bucket.count = bucket.breaches.Count;

                return new GetBreachAgingReportDto
                {
                    hasCurrentEvaluation = true,
                    evaluationId = currentEvaluation.evaluationId,
                    buckets = buckets,
                };
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }
    }
}
