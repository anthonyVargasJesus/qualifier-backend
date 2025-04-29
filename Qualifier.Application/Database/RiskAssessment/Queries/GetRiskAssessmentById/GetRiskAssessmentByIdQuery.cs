using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.RiskAssessment.Queries.GetRiskAssessmentById
{
    public class GetRiskAssessmentByIdQuery : IGetRiskAssessmentByIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetRiskAssessmentByIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int riskAssessmentId)
        {
            try
            {
                var entity = await (from item in _databaseService.RiskAssessment
                                    where ((item.isDeleted == null || item.isDeleted == false) && item.riskAssessmentId == riskAssessmentId)
                                    select new RiskAssessmentEntity()
                                    {
                                        riskAssessmentId = item.riskAssessmentId,
                                        riskId = item.riskId,
                                        valuationCID = item.valuationCID,
                                        menaceLevelValue = item.menaceLevelValue,
                                        vulnerabilityLevelValue = item.vulnerabilityLevelValue,
                                        existingImplementedControls = item.existingImplementedControls,
                                        riskAssessmentValue = item.riskAssessmentValue,
                                        riskLevelId = item.riskLevelId,
                                    }).FirstOrDefaultAsync();

                return _mapper.Map<GetRiskAssessmentByIdDto>(entity);
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

