using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.Risk.Queries.GetAllRisksByEvaluationId
{
    internal class GetAllRisksByEvaluationIdQuery : IGetAllRisksByEvaluationIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        public GetAllRisksByEvaluationIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }

        public async Task<Object> Execute(int evaluationId)
        {
            try
            {
                var entities = await (from risk in _databaseService.Risk
                                      where ((risk.isDeleted == null || risk.isDeleted == false) && risk.evaluationId == evaluationId)
                                      select new RiskEntity
                                      {
                                          riskId = risk.riskId,
                                          name = risk.name,
                                          evaluationId = risk.evaluationId,
                                          activesInventoryId = risk.activesInventoryId,
                                          activesInventoryNumber = risk.activesInventoryNumber,
                                          activesInventoryName = risk.activesInventoryName,
                                          menaceId = risk.menaceId,
                                          vulnerabilityId = risk.vulnerabilityId,
                                          companyId = risk.companyId,
                                      }).ToListAsync();

                BaseResponseDto<GetAllRisksByEvaluationIdDto> baseResponseDto = new BaseResponseDto<GetAllRisksByEvaluationIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetAllRisksByEvaluationIdDto>>(entities);
                return baseResponseDto;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

