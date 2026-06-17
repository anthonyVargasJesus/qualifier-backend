using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.Breach.Queries.GetBreachSeverityReport
{
    internal class GetBreachSeverityReportQuery : IGetBreachSeverityReportQuery
    {
        private readonly IDatabaseService _databaseService;

        public GetBreachSeverityReportQuery(IDatabaseService databaseService)
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
                    return new GetBreachSeverityReportDto { hasCurrentEvaluation = false };

                var breaches = await (from b in _databaseService.Breach
                                      join severity in _databaseService.BreachSeverity
                                          on b.breachSeverityId equals severity.breachSeverityId
                                      join status in _databaseService.BreachStatus
                                          on b.breachStatusId equals status.breachStatusId
                                      join responsible in _databaseService.Responsible
                                          on b.responsibleId equals responsible.responsibleId
                                      where b.companyId == companyId
                                            && b.evaluationId == currentEvaluation.evaluationId
                                      select new BreachEntity
                                      {
                                          breachId = b.breachId,
                                          title = b.title,
                                          numerationToShow = b.numerationToShow,
                                          type = b.type,
                                          breachSeverity = new BreachSeverityEntity
                                          {
                                              name = severity.name,
                                              abbreviation = severity.abbreviation,
                                              color = severity.color,
                                              value = severity.value,
                                          },
                                          breachStatus = new BreachStatusEntity
                                          {
                                              name = status.name,
                                              abbreviation = status.abbreviation,
                                              color = status.color,
                                              value = status.value,
                                          },
                                          responsible = new ResponsibleEntity { name = responsible.name },
                                      }).ToListAsync();

                if (!breaches.Any())
                    return new GetBreachSeverityReportDto
                    {
                        hasCurrentEvaluation = true,
                        evaluationId = currentEvaluation.evaluationId,
                        evaluationDescription = currentEvaluation.description,
                        evaluationStartDate = currentEvaluation.startDate,
                        evaluationEndDate = currentEvaluation.endDate,
                    };

                // Breach IDs that have at least one action plan in this evaluation
                var breachIdsWithPlan = await (from ap in _databaseService.ActionPlan
                                               where (ap.isDeleted == null || ap.isDeleted == false)
                                                     && ap.companyId == companyId
                                                     && ap.evaluationId == currentEvaluation.evaluationId
                                               group ap by ap.breachId into g
                                               select new { breachId = g.Key, count = g.Count() })
                                              .ToListAsync();

                var maxSeverityValue = breaches.Max(b => b.breachSeverity!.value);
                var topSeverity = breaches.First(b => b.breachSeverity!.value == maxSeverityValue).breachSeverity!;

                var breachDtos = breaches
                    .OrderByDescending(b => b.breachSeverity!.value)
                    .Select(b =>
                    {
                        var planEntry = breachIdsWithPlan.FirstOrDefault(p => p.breachId == b.breachId);
                        return new GetBreachSeverityReportBreachDto
                        {
                            breachId = b.breachId,
                            title = b.title,
                            numerationToShow = b.numerationToShow,
                            type = b.type,
                            severityName = b.breachSeverity!.name,
                            severityAbbreviation = b.breachSeverity.abbreviation,
                            severityColor = b.breachSeverity.color,
                            severityValue = b.breachSeverity.value,
                            statusName = b.breachStatus!.name,
                            statusAbbreviation = b.breachStatus.abbreviation,
                            statusColor = b.breachStatus.color,
                            responsibleName = b.responsible!.name,
                            actionPlanCount = planEntry?.count ?? 0,
                        };
                    }).ToList();

                var severityGroups = breaches
                    .GroupBy(b => new { b.breachSeverity!.name, b.breachSeverity.color })
                    .OrderByDescending(g => g.First().breachSeverity!.value)
                    .ToList();

                var severityChart = severityGroups
                    .Select(g => new GetBreachSeverityReportPieDto { name = g.Key.name, value = g.Count() })
                    .ToList();

                var severityColors = severityGroups.Select(g => g.Key.color).ToList();

                var statusGroups = breaches
                    .GroupBy(b => new { b.breachStatus!.name, b.breachStatus.color })
                    .OrderByDescending(g => g.Count())
                    .ToList();

                var statusChart = statusGroups
                    .Select(g => new GetBreachSeverityReportPieDto { name = g.Key.name, value = g.Count() })
                    .ToList();

                var statusColors = statusGroups.Select(g => g.Key.color).ToList();

                return new GetBreachSeverityReportDto
                {
                    hasCurrentEvaluation = true,
                    evaluationId = currentEvaluation.evaluationId,
                    evaluationDescription = currentEvaluation.description,
                    evaluationStartDate = currentEvaluation.startDate,
                    evaluationEndDate = currentEvaluation.endDate,
                    totalBreaches = breaches.Count,
                    withoutActionPlan = breachDtos.Count(b => b.actionPlanCount == 0),
                    highestSeverityCount = breaches.Count(b => b.breachSeverity!.value == maxSeverityValue),
                    highestSeverityName = topSeverity.name,
                    highestSeverityColor = topSeverity.color,
                    totalResponsibles = breaches.Select(b => b.responsibleId).Distinct().Count(),
                    breaches = breachDtos,
                    severityChart = severityChart,
                    statusChart = statusChart,
                    severityColors = severityColors,
                    statusColors = statusColors,
                };
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }
    }
}
