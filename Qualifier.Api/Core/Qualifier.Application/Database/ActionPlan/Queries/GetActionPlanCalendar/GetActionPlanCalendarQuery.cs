using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Service;

namespace Qualifier.Application.Database.ActionPlan.Queries.GetActionPlanCalendar
{
    // "Plan de Acción / Implementación", vista calendario: un plan de acción por fila con su
    // rango de fechas (D_START_DATE/D_DUE_DATE), en vez de la grilla mensual manual de la hoja
    // "Implementación" del Excel de referencia -- las fechas ya existen en el sistema (a
    // diferencia de la hoja "Revisión", que necesitaría un concepto nuevo), así que esto es
    // pura lectura/agregación, sin cambios de esquema.
    //
    // "items" pagina (Skip/Take) porque puede haber muchos planes de acción por evaluación;
    // "statusSummary" NO pagina y trae una fila por cada estado del catálogo
    // (MAE_ACTION_PLAN_STATUS de la empresa) con su conteo mensual sobre el total -- se decidió
    // así (en vez de un "Planificado/Ejecutado/Avance %" fijo) para no inventar categorías que
    // no existen en el catálogo real de la empresa.
    public class GetActionPlanCalendarQuery : IGetActionPlanCalendarQuery
    {
        private readonly IDatabaseService _databaseService;

        public GetActionPlanCalendarQuery(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public async Task<Object> Execute(int companyId, int evaluationId, int year, int skip, int pageSize)
        {
            try
            {
                var allItems = await (
                    from ap in _databaseService.ActionPlan
                    join breach in _databaseService.Breach on ap.breachId equals breach.breachId
                    join status in _databaseService.ActionPlanStatus on ap.actionPlanStatusId equals status.actionPlanStatusId
                    join user in _databaseService.User on ap.userId equals user.userId into userJoin
                    from user in userJoin.DefaultIfEmpty()
                    where (ap.isDeleted == null || ap.isDeleted == false)
                        && ap.evaluationId == evaluationId
                        && ap.companyId == companyId
                    orderby breach.numerationToShow, ap.startDate
                    select new GetActionPlanCalendarItemDto
                    {
                        actionPlanId = ap.actionPlanId,
                        controlCode = breach.numerationToShow ?? "",
                        controlName = breach.title,
                        title = ap.title,
                        responsibleName = user == null ? null : ((user.name ?? "") + " " + (user.firstName ?? "")).Trim(),
                        startDate = ap.startDate,
                        dueDate = ap.dueDate,
                        statusId = status.actionPlanStatusId,
                        statusName = status.name,
                        statusAbbreviation = status.abbreviation,
                        statusColor = status.color,
                    }
                ).ToListAsync();

                var allStatuses = await _databaseService.ActionPlanStatus
                    .Where(s => (s.isDeleted == null || s.isDeleted == false) && s.companyId == companyId)
                    .OrderBy(s => s.value)
                    .ToListAsync();

                var statusSummary = BuildStatusSummary(allItems, allStatuses, year);
                var page = allItems.Skip(skip).Take(pageSize).ToList();

                return new GetActionPlanCalendarResponseDto
                {
                    items = page,
                    pagination = Pagination.GetPagination(allItems.Count, pageSize),
                    statusSummary = statusSummary,
                };
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

        private static List<GetActionPlanCalendarStatusSummaryDto> BuildStatusSummary(
            List<GetActionPlanCalendarItemDto> items, List<Domain.Entities.ActionPlanStatusEntity> statuses, int year)
        {
            var monthRanges = Enumerable.Range(0, 12)
                .Select(m => (start: new DateTime(year, m + 1, 1), end: new DateTime(year, m + 1, 1).AddMonths(1).AddDays(-1)))
                .ToList();

            return statuses.Select(status =>
            {
                var itemsInStatus = items.Where(i => i.statusId == status.actionPlanStatusId).ToList();
                var counts = monthRanges
                    .Select(range => itemsInStatus.Count(i => i.startDate <= range.end && i.dueDate >= range.start))
                    .ToArray();

                return new GetActionPlanCalendarStatusSummaryDto
                {
                    statusId = status.actionPlanStatusId,
                    statusName = status.name,
                    statusColor = status.color,
                    monthCounts = counts,
                };
            }).ToList();
        }
    }
}
