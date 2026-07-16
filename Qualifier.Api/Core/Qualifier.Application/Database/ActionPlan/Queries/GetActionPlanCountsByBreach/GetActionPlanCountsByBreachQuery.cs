using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Service;

namespace Qualifier.Application.Database.ActionPlan.Queries.GetActionPlanCountsByBreach
{
    // Resumen de "Plan de acción" (dueño del control): en vez de que el cliente pida las
    // acciones de cada brecha una por una (una llamada por brecha, hasta 20+ en una
    // evaluación grande) solo para armar el contador "X/Y acciones completadas", se agregan
    // acá server-side con un solo GROUP BY — una llamada, un round trip a la base.
    internal class GetActionPlanCountsByBreachQuery : IGetActionPlanCountsByBreachQuery
    {
        private readonly IDatabaseService _databaseService;

        public GetActionPlanCountsByBreachQuery(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public async Task<Object> Execute(int companyId, int evaluationId)
        {
            try
            {
                // "Completada" = el estado con mayor `value` del catálogo de la empresa —
                // mismo criterio que ya usaba el cliente (_maxValueStatusId en el Flutter).
                var maxValueStatusId = await (
                    from status in _databaseService.ActionPlanStatus
                    where (status.isDeleted == null || status.isDeleted == false)
                        && status.companyId == companyId
                    orderby status.value descending
                    select status.actionPlanStatusId
                ).FirstOrDefaultAsync();

                var counts = await (
                    from ap in _databaseService.ActionPlan
                    where (ap.isDeleted == null || ap.isDeleted == false)
                        && ap.companyId == companyId
                        && ap.evaluationId == evaluationId
                    group ap by ap.breachId into g
                    select new GetActionPlanCountsByBreachItemDto
                    {
                        breachId = g.Key,
                        total = g.Count(),
                        completed = g.Count(x => x.actionPlanStatusId == maxValueStatusId),
                    }
                ).ToListAsync();

                return new GetActionPlanCountsByBreachDto { counts = counts };
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }
    }
}
