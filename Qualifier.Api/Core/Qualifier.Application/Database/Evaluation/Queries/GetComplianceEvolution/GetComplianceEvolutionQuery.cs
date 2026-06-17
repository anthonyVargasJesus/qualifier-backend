using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.Evaluation.Queries.GetComplianceEvolution
{
    internal class GetComplianceEvolutionQuery : IGetComplianceEvolutionQuery
    {
        private readonly IDatabaseService _databaseService;

        public GetComplianceEvolutionQuery(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public async Task<Object> Execute(int companyId)
        {
            try
            {
                const decimal MAX_VALUE = 5.0m;

                var evaluations = await (from eval in _databaseService.Evaluation
                                         where (eval.isDeleted == null || eval.isDeleted == false)
                                               && eval.companyId == companyId
                                         orderby eval.startDate
                                         select new EvaluationEntity
                                         {
                                             evaluationId = eval.evaluationId,
                                             description = eval.description,
                                             startDate = eval.startDate,
                                             endDate = eval.endDate,
                                             isCurrent = eval.isCurrent,
                                         }).ToListAsync();

                if (!evaluations.Any())
                    return new GetComplianceEvolutionDto { totalEvaluations = 0 };

                var reqData = await (from re in _databaseService.RequirementEvaluation
                                     where re.companyId == companyId
                                     group re by re.evaluationId into g
                                     select new
                                     {
                                         evaluationId = g.Key,
                                         avgValue = g.Average(x => x.value),
                                         count = g.Count(),
                                     }).ToListAsync();

                var ctrlData = await (from ce in _databaseService.ControlEvaluation
                                      where ce.companyId == companyId
                                      group ce by ce.evaluationId into g
                                      select new
                                      {
                                          evaluationId = g.Key,
                                          avgValue = g.Average(x => x.value),
                                          count = g.Count(),
                                      }).ToListAsync();

                var items = evaluations.Select(eval =>
                {
                    var req = reqData.FirstOrDefault(r => r.evaluationId == eval.evaluationId);
                    var ctrl = ctrlData.FirstOrDefault(c => c.evaluationId == eval.evaluationId);

                    decimal? reqRate = req != null ? Math.Round(req.avgValue / MAX_VALUE * 100, 1) : (decimal?)null;
                    decimal? ctrlRate = ctrl != null ? Math.Round(ctrl.avgValue / MAX_VALUE * 100, 1) : (decimal?)null;
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
                        requirementsCount = req?.count ?? 0,
                        controlsCount = ctrl?.count ?? 0,
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

                var currentItem = items.FirstOrDefault(i => i.isCurrent);
                var currentIndex = currentItem != null ? items.IndexOf(currentItem) : -1;
                var previousItem = currentIndex > 0 ? items[currentIndex - 1] : null;

                return new GetComplianceEvolutionDto
                {
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
