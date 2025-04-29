using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.Risk.Queries.GetRiskById
{
    public class GetRiskByIdQuery : IGetRiskByIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetRiskByIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int riskId)
        {
            try
            {
                var risk = await (from item in _databaseService.Risk
                                    where ((item.isDeleted == null || item.isDeleted == false) && item.riskId == riskId)
                                    select new RiskEntity()
                                    {
                                        riskId = item.riskId,
                                        evaluationId = item.evaluationId,
                                        activesInventoryId = item.activesInventoryId,
                                        activesInventoryNumber = item.activesInventoryNumber,
                                        activesInventoryName = item.activesInventoryName,
                                        menaceId = item.menaceId,
                                        vulnerabilityId = item.vulnerabilityId,
                                        companyId = item.companyId,
                                    }).FirstOrDefaultAsync();

                var riskAssessment = await (from item in _databaseService.RiskAssessment
                                      where ((item.isDeleted == null || item.isDeleted == false) && item.riskId == riskId)
                                      select new RiskAssessmentEntity
                                      {
                                          riskAssessmentId = item.riskAssessmentId,

                                      }).FirstOrDefaultAsync();

                var riskDto = _mapper.Map<GetRiskByIdDto>(risk);
                if (riskAssessment != null)
                    riskDto.riskAssessmentId = riskAssessment.riskAssessmentId;

                return riskDto;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

