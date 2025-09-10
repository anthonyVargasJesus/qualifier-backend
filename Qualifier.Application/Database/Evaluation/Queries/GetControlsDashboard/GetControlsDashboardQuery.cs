using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Application.Database.Evaluation.Queries.GetDashboard;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qualifier.Application.Database.Evaluation.Queries.GetControlsDashboard
{
    public class GetControlsDashboardQuery : IGetControlsDashboardQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetControlsDashboardQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int standardId, int evaluationId, int companyId)
        {
            try
            {

                var maturityLevels = await (from maturityLevel in _databaseService.MaturityLevel
                                            where ((maturityLevel.isDeleted == null || maturityLevel.isDeleted == false) && maturityLevel.companyId == companyId)
                                            select new MaturityLevelEntity
                                            {
                                                maturityLevelId = maturityLevel.maturityLevelId,
                                                name = maturityLevel.name,
                                                abbreviation = maturityLevel.abbreviation,
                                                value = maturityLevel.value,
                                                color = maturityLevel.color,
                                                factor = maturityLevel.factor,
                                            }).OrderByDescending(x => x.value).ToListAsync();

                var indicators = await (from indicator in _databaseService.Indicator
                                        where ((indicator.isDeleted == null || indicator.isDeleted == false) && indicator.companyId == companyId)
                                        select new IndicatorEntity
                                        {
                                            indicatorId = indicator.indicatorId,
                                            name = indicator.name,
                                            description = indicator.description,
                                            abbreviation = indicator.abbreviation,
                                            minimum = indicator.minimum,
                                            maximum = indicator.maximum,
                                            color = indicator.color,
                                            factor = indicator.factor == null ? 0 : indicator.factor,
                                        }).ToListAsync();

                var controlGroups = await (from controlGroup in _databaseService.ControlGroup
                                           where ((controlGroup.isDeleted == null || controlGroup.isDeleted == false) && controlGroup.standardId == standardId)
                                           select new ControlGroupEntity
                                           {
                                               controlGroupId = controlGroup.controlGroupId,
                                               number = controlGroup.number,
                                               name = controlGroup.name,
                                           }).ToListAsync();


                var controlEvaluations = await (from controlEvaluation in _databaseService.ControlEvaluation
                                                join control in _databaseService.Control on controlEvaluation.control equals control
                                                where ((controlEvaluation.isDeleted == null || controlEvaluation.isDeleted == false) && controlEvaluation.evaluationId == evaluationId)
                                                select new ControlEvaluationEntity
                                                {
                                                    maturityLevelId = controlEvaluation.maturityLevelId,
                                                    value = controlEvaluation.value,
                                                    controlGroupId = controlEvaluation.control.controlGroupId,
                                                }).ToListAsync();


                var standardEntity = new StandardEntity();
                standardEntity.controlsGroups = controlGroups;
                standardEntity.setTotalValuesToControls(controlEvaluations, maturityLevels);

                standardEntity.setTotalValueControlsInMaturityLevels(maturityLevels);

                decimal totalValuexEvaluation = standardEntity.getTotalControlValuexEvaluation();

                standardEntity.setControlIndicators(indicators);

                standardEntity.setPercentControlsInMaturityLevels(maturityLevels, totalValuexEvaluation);

                List<PieControlDashboardControlGroup> pieControlDashboardControlGroups = standardEntity.getPieControlChartDashboard(maturityLevels);

                List<string> colors = standardEntity.getColors(maturityLevels);

                List<BartVerticalControlDashboard> bartVerticalControlDashboard = standardEntity.getControlBarChartDashboard();


                GetControlDashboardDto data = new GetControlDashboardDto();
                data.maturityLevels = _mapper.Map<List<GetControlDashboardMaturityLevelDto>>(maturityLevels).ToList();
                data.controlGroups = _mapper.Map<List<GetControlDashboardControlGroupDto>>(standardEntity.controlsGroups).ToList();

                data.value = totalValuexEvaluation;
                data.pieControlDashboardControlGroup = _mapper.Map<List<GetPieControlDashboardControlGroupDto>>(pieControlDashboardControlGroups).ToList();
                data.bartVerticalDashboardControlGroupDto = _mapper.Map<List<GetBartVerticalControlDashboardDto>>(bartVerticalControlDashboard).ToList();
                data.colors = colors;
                data.indicators = getData(_mapper.Map<List<GetDashboardControlIndicatorDto>>(indicators).ToList(), _mapper.Map<List<GetControlDashboardControlGroupDto>>(standardEntity.controlsGroups).ToList());
                data.controlsIndicator = getControlGroupIndicator(_mapper.Map<List<GetControlDashboardMaturityLevelInControlIndicatorDto>>(indicators), data.controlGroups);

                return data;
            }
            catch (Exception )
            {
             return BaseApplication.getExceptionErrorResponse();
            }
        }

        List<GetDashboardControlIndicatorDto> getData(List<GetDashboardControlIndicatorDto> indicators, List<GetControlDashboardControlGroupDto> groups)
        {
            List<GetDashboardControlIndicatorDto> result = new List<GetDashboardControlIndicatorDto>();

            var stateIndicator = new GetDashboardControlIndicatorDto();
            stateIndicator.name = "Estado";
            stateIndicator.color = "#0070c0";
            indicators.Add(stateIndicator);

            foreach (var indicator in indicators)
            {
                var series = new List<GetControlDashboardMaturityLevelInControlIndicatorDto>();

                foreach (var group in groups)
                {
                    var serie = new GetControlDashboardMaturityLevelInControlIndicatorDto();
                    serie.name = "Anexo " + group.number;

                    // 📌 Caso 1: Estado real (usamos requirement.value)
                    if (indicator.name == "Estado")
                        serie.value = group.indicatorValue;
                    else
                        serie.value = indicator.factor;

                    series.Add(serie);
                }

                result.Add(new GetDashboardControlIndicatorDto
                {
                    name = indicator.name,
                    color = indicator.color,
                    series = series
                });
            }

            return result;
        }


        GetControlDashboardMaturityLevelInControlIndicatorDto getControlGroupIndicator(List<GetControlDashboardMaturityLevelInControlIndicatorDto> indicators, List<GetControlDashboardControlGroupDto> groups)
        {
            GetControlDashboardMaturityLevelInControlIndicatorDto result = new GetControlDashboardMaturityLevelInControlIndicatorDto();
            result.minimum = indicators.Min(x => x.factor);
            result.maximum = indicators.Max(x => x.factor);
            result.value = groups.Average(x => x.indicatorValue);

            foreach (GetControlDashboardMaturityLevelInControlIndicatorDto item in indicators)
                if (item.minimum != null && item.maximum != null)
                    if (result.value > item.minimum.Value && result.value <= item.maximum.Value)
                    {
                        result.name = item.name;
                        result.color = item.color;
                    }

            return result;
        }

    }
}
