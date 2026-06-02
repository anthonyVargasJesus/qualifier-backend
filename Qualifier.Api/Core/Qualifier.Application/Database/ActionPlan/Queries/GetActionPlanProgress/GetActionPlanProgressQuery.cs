using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.ActionPlan.Queries.GetActionPlanProgress
{
    internal class GetActionPlanProgressQuery : IGetActionPlanProgressQuery
    {
        private readonly IDatabaseService _databaseService;

        public GetActionPlanProgressQuery(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public async Task<Object> Execute(int companyId)
        {
            try
            {
                var currentEvaluation = await (from eval in _databaseService.Evaluation
                                               where (eval.isDeleted == null || eval.isDeleted == false)
                                                     && eval.companyId == companyId
                                                     && eval.isCurrent
                                               select new EvaluationEntity
                                               {
                                                   evaluationId = eval.evaluationId,
                                                   description = eval.description,
                                                   startDate = eval.startDate,
                                                   endDate = eval.endDate,
                                               }).FirstOrDefaultAsync();

                if (currentEvaluation == null)
                    return new GetActionPlanProgressDto { hasCurrentEvaluation = false };

                var plans = await (from ap in _databaseService.ActionPlan
                                   join status in _databaseService.ActionPlanStatus
                                       on ap.actionPlanStatusId equals status.actionPlanStatusId
                                   join priority in _databaseService.ActionPlanPriority
                                       on ap.actionPlanPriorityId equals priority.actionPlanPriorityId
                                   join responsible in _databaseService.Responsible
                                       on ap.responsibleId equals responsible.responsibleId
                                   where (ap.isDeleted == null || ap.isDeleted == false)
                                         && ap.companyId == companyId
                                         && ap.evaluationId == currentEvaluation.evaluationId
                                   select new ActionPlanEntity
                                   {
                                       actionPlanId = ap.actionPlanId,
                                       dueDate = ap.dueDate,
                                       responsibleId = ap.responsibleId,
                                       actionPlanStatusId = ap.actionPlanStatusId,
                                       actionPlanPriorityId = ap.actionPlanPriorityId,
                                       responsible = new ResponsibleEntity { name = responsible.name },
                                       actionPlanStatus = new ActionPlanStatusEntity
                                       {
                                           name = status.name,
                                           abbreviation = status.abbreviation,
                                           color = status.color,
                                           value = status.value,
                                       },
                                       actionPlanPriority = new ActionPlanPriorityEntity
                                       {
                                           name = priority.name,
                                           abbreviation = priority.abbreviation,
                                           color = priority.color,
                                           value = priority.value,
                                       },
                                   }).ToListAsync();

                if (!plans.Any())
                    return new GetActionPlanProgressDto
                    {
                        hasCurrentEvaluation = true,
                        evaluationId = currentEvaluation.evaluationId,
                        evaluationDescription = currentEvaluation.description,
                        evaluationStartDate = currentEvaluation.startDate,
                        evaluationEndDate = currentEvaluation.endDate,
                    };

                var today = DateTime.UtcNow.Date;
                var maxStatusValue = plans.Max(p => p.actionPlanStatus!.value);

                var responsibleGroups = plans
                    .GroupBy(p => new { p.responsibleId, name = p.responsible!.name })
                    .OrderBy(g => g.Key.name)
                    .Select(g =>
                    {
                        var plansInGroup = g.ToList();
                        var completed = plansInGroup.Count(p => p.actionPlanStatus!.value == maxStatusValue);
                        var overdue = plansInGroup.Count(p =>
                            p.dueDate.Date < today && p.actionPlanStatus!.value != maxStatusValue);
                        var total = plansInGroup.Count;

                        var statusBreakdown = plansInGroup
                            .GroupBy(p => new
                            {
                                p.actionPlanStatus!.name,
                                p.actionPlanStatus.abbreviation,
                                p.actionPlanStatus.color
                            })
                            .OrderByDescending(s => s.Sum(p => p.actionPlanStatus!.value))
                            .Select(s => new GetActionPlanProgressStatusDetailDto
                            {
                                statusName = s.Key.name,
                                abbreviation = s.Key.abbreviation,
                                color = s.Key.color,
                                count = s.Count(),
                            }).ToList();

                        return new GetActionPlanProgressResponsibleDto
                        {
                            responsibleId = g.Key.responsibleId,
                            responsibleName = g.Key.name,
                            total = total,
                            completed = completed,
                            overdue = overdue,
                            completionRate = total > 0 ? Math.Round((decimal)completed / total * 100, 1) : 0,
                            statusBreakdown = statusBreakdown,
                        };
                    }).ToList();

                // Pie: distribución por estado
                var statusChart = plans
                    .GroupBy(p => p.actionPlanStatus!.name)
                    .Select(g => new GetActionPlanProgressPieDto { name = g.Key, value = g.Count() })
                    .ToList();

                var statusColors = plans
                    .GroupBy(p => p.actionPlanStatus!.name)
                    .Select(g => g.First().actionPlanStatus!.color)
                    .ToList();

                // Pie: distribución por prioridad
                var priorityChart = plans
                    .GroupBy(p => p.actionPlanPriority!.name)
                    .OrderByDescending(g => g.First().actionPlanPriority!.value)
                    .Select(g => new GetActionPlanProgressPieDto { name = g.Key, value = g.Count() })
                    .ToList();

                var priorityColors = plans
                    .GroupBy(p => p.actionPlanPriority!.name)
                    .OrderByDescending(g => g.First().actionPlanPriority!.value)
                    .Select(g => g.First().actionPlanPriority!.color)
                    .ToList();

                // Stacked bar: planes por responsable agrupados por estado
                var allStatusNames = plans
                    .Select(p => p.actionPlanStatus!.name)
                    .Distinct()
                    .OrderByDescending(name => plans.First(p => p.actionPlanStatus!.name == name).actionPlanStatus!.value)
                    .ToList();

                var responsibleStackedChart = responsibleGroups.Select(r => new GetActionPlanProgressStackedDto
                {
                    name = r.responsibleName,
                    series = allStatusNames.Select(statusName => new GetActionPlanProgressPieDto
                    {
                        name = statusName,
                        value = r.statusBreakdown.FirstOrDefault(s => s.statusName == statusName)?.count ?? 0,
                    }).ToList(),
                }).ToList();

                var totalCompleted = responsibleGroups.Sum(r => r.completed);
                var total = plans.Count;

                return new GetActionPlanProgressDto
                {
                    hasCurrentEvaluation = true,
                    evaluationId = currentEvaluation.evaluationId,
                    evaluationDescription = currentEvaluation.description,
                    evaluationStartDate = currentEvaluation.startDate,
                    evaluationEndDate = currentEvaluation.endDate,
                    totalPlans = total,
                    totalResponsibles = responsibleGroups.Count,
                    overdueCount = responsibleGroups.Sum(r => r.overdue),
                    globalCompletionRate = total > 0 ? Math.Round((decimal)totalCompleted / total * 100, 1) : 0,
                    responsibles = responsibleGroups.OrderByDescending(r => r.overdue).ThenBy(r => r.completionRate).ToList(),
                    statusChart = statusChart,
                    priorityChart = priorityChart,
                    responsibleStackedChart = responsibleStackedChart,
                    statusColors = statusColors,
                    priorityColors = priorityColors,
                };
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }
    }
}
