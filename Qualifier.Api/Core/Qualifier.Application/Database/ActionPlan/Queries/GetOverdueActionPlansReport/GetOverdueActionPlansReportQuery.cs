using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Service;

namespace Qualifier.Application.Database.ActionPlan.Queries.GetOverdueActionPlansReport
{
    // Reporte "Acciones vencidas (detalle)" para el dueño del requisito: complementa el conteo
    // agregado que ya ve en "Carga por responsable" mostrando cuáles acciones son, de quién y
    // hace cuántos días vencieron — mismo patrón de buckets que "Antigüedad de brechas".
    // Mismo criterio de "vencida" que ya usan GetGapDashboardQuery/GetActionPlanCountsByUserQuery
    // y la app móvil (task_list_options.dart): dueDate pasada y estado no cerrado (COMP/CERR).
    public class GetOverdueActionPlansReportQuery : IGetOverdueActionPlansReportQuery
    {
        private const string COMPLETADO = "COMP";
        private const string CERRADO = "CERR";
        private const int BUCKET_1_MAX_DAYS = 7;
        private const int BUCKET_2_MAX_DAYS = 30;

        private readonly IDatabaseService _databaseService;

        public GetOverdueActionPlansReportQuery(IDatabaseService databaseService)
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
                    return new GetOverdueActionPlansReportDto { hasCurrentEvaluation = false };

                var today = DateTime.UtcNow.Date;

                var plans = await (
                    from ap in _databaseService.ActionPlan
                    join status in _databaseService.ActionPlanStatus on ap.actionPlanStatusId equals status.actionPlanStatusId
                    join breach in _databaseService.Breach on ap.breachId equals breach.breachId
                    join user in _databaseService.User on ap.userId equals user.userId into userJoin
                    from user in userJoin.DefaultIfEmpty()
                    where (ap.isDeleted == null || ap.isDeleted == false)
                        && ap.companyId == companyId
                        && ap.evaluationId == currentEvaluation.evaluationId
                        && ap.dueDate.Date < today
                        && status.abbreviation != COMPLETADO && status.abbreviation != CERRADO
                    select new
                    {
                        ap.actionPlanId,
                        ap.title,
                        breachTitle = breach.title,
                        breach.numerationToShow,
                        responsibleName = user == null ? "Sin asignar" : (user.name ?? "") + " " + (user.firstName ?? ""),
                        ap.dueDate,
                    }
                ).ToListAsync();

                var items = plans
                    .Select(p => new GetOverdueActionPlanItemDto
                    {
                        actionPlanId = p.actionPlanId,
                        title = p.title,
                        breachTitle = p.breachTitle,
                        breachNumerationToShow = p.numerationToShow,
                        responsibleName = p.responsibleName,
                        daysOverdue = (today - p.dueDate.Date).Days,
                    })
                    .OrderByDescending(i => i.daysOverdue)
                    .ToList();

                var buckets = new List<GetOverdueActionPlansBucketDto>
                {
                    new GetOverdueActionPlansBucketDto
                    {
                        label = "1-7 días",
                        items = items.Where(i => i.daysOverdue <= BUCKET_1_MAX_DAYS).ToList(),
                    },
                    new GetOverdueActionPlansBucketDto
                    {
                        label = "8-30 días",
                        items = items.Where(i => i.daysOverdue > BUCKET_1_MAX_DAYS && i.daysOverdue <= BUCKET_2_MAX_DAYS).ToList(),
                    },
                    new GetOverdueActionPlansBucketDto
                    {
                        label = "Más de 30 días",
                        items = items.Where(i => i.daysOverdue > BUCKET_2_MAX_DAYS).ToList(),
                    },
                };
                foreach (var bucket in buckets)
                    bucket.count = bucket.items.Count;

                return new GetOverdueActionPlansReportDto
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
