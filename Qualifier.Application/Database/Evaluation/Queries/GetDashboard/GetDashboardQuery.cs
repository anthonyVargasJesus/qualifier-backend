using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.Evaluation.Queries.GetDashboard
{
    internal class GetDashboardQuery : IGetDashboardQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetDashboardQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int standardId, int evaluationId, int companyId)
        {
            try
            {

                var evaluations = await (from requirementEvaluation in _databaseService.RequirementEvaluation
                                         where ((requirementEvaluation.isDeleted == null || requirementEvaluation.isDeleted == false) && requirementEvaluation.evaluationId == evaluationId)
                                         select new RequirementEvaluationEntity
                                         {
                                             maturityLevelId = requirementEvaluation.maturityLevelId,
                                             value = requirementEvaluation.value,
                                             requirementId = requirementEvaluation.requirementId,
                                         }).ToListAsync();

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
                                        }).ToListAsync();

                const int FIRST_LEVEL = 1;

                var requirements = await (from requirement in _databaseService.Requirement
                                          where ((requirement.isDeleted == null || requirement.isDeleted == false) && requirement.standardId == standardId
                                          && requirement.isEvaluable)
                                          select new RequirementEntity
                                          {
                                              requirementId = requirement.requirementId,
                                              numeration = requirement.numeration,
                                              name = requirement.name,
                                              description = requirement.description,
                                              level = requirement.level,
                                              parentId = requirement.parentId,
                                          }).ToListAsync();

                //Dashboard requirements
                var parentRequirements = requirements.Where(x => x.level == FIRST_LEVEL).ToList();

                var standardEntity = new StandardEntity();
                standardEntity.setRequirementsWithChildren(requirements);
                standardEntity.setTotalValuesToRequirements(evaluations, maturityLevels);

                standardEntity.setTotalValueInMaturityLevels(maturityLevels);

                decimal totalValuexEvaluation = standardEntity.getTotalValuexEvaluation();

                standardEntity.setIndicators(indicators);

                standardEntity.setPercentInMaturityLevels(maturityLevels, totalValuexEvaluation);

                List<PieDashboardRequirement> pieDashboardRequirements = standardEntity.getPieChartDashboard(maturityLevels);

                List<string> colors = standardEntity.getColors(maturityLevels);

                List<BartVerticalDashboardRequirement> bartVerticalDashboardRequirements = standardEntity.getBarChartDashboard();


                GetDashboardDto data = new GetDashboardDto();
                data.maturityLevels = _mapper.Map<List<GetDashboardMaturityLevelDto>>(maturityLevels).ToList();
                data.requirements = _mapper.Map<List<GetDashboardRequirementDto>>(standardEntity.requirements).ToList();
                data.value = totalValuexEvaluation;
                data.pieDashboardRequirementDto = _mapper.Map<List<GetPieDashboardRequirementDto>>(pieDashboardRequirements).ToList();
                data.bartVerticalDashboardRequirementDto = _mapper.Map<List<GetBartVerticalDashboardRequirementDto>>(bartVerticalDashboardRequirements).ToList();
                data.colors = colors;

                return data;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }



    }
}
