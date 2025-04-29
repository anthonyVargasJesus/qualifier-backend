using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.Risk.Queries.GetRisksByEvaluationId
{
    public class GetRisksByEvaluationIdQuery : IGetRisksByEvaluationIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetRisksByEvaluationIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int skip, int pageSize, string search, int evaluationId)
        {
            try
            {
                var entities = await (from risk in _databaseService.Risk
                                      join menace in _databaseService.Menace on risk.menace equals menace
                                      join vulnerability in _databaseService.Vulnerability on risk.vulnerability equals vulnerability
                                      where ((risk.isDeleted == null || risk.isDeleted == false) && risk.evaluationId == evaluationId)
                                      && (risk.activesInventoryName.ToUpper().Contains(search.ToUpper()))
                                      select new RiskEntity
                                      {
                                          riskId = risk.riskId,
                                          activesInventoryId = risk.activesInventoryId,
                                          activesInventoryNumber = risk.activesInventoryNumber,
                                          activesInventoryName = risk.activesInventoryName,
                                          menace = new MenaceEntity
                                          {
                                              name = menace.name,
                                          },
                                          vulnerability = new VulnerabilityEntity
                                          {
                                              name = vulnerability.name,
                                          },
                                      })
                .Skip(skip).Take(pageSize)
                .ToListAsync();

                BaseResponseDto<GetRisksByEvaluationIdDto> baseResponseDto = new BaseResponseDto<GetRisksByEvaluationIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetRisksByEvaluationIdDto>>(entities);
                baseResponseDto.pagination = Pagination.GetPagination(await getTotal(search, evaluationId), pageSize);
                return baseResponseDto;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

        public async Task<int> getTotal(string search, int evaluationId)
        {
            var total = await (from risk in _databaseService.Risk
                               join menace in _databaseService.Menace on risk.menace equals menace
                               join vulnerability in _databaseService.Vulnerability on risk.vulnerability equals vulnerability
                               where ((risk.isDeleted == null || risk.isDeleted == false) && risk.evaluationId == evaluationId)
                               && (risk.activesInventoryName.ToUpper().Contains(search.ToUpper()))
                               select new RiskEntity
                               {
                                   riskId = risk.riskId,
                               }).CountAsync();
            return total;
        }

    }
}
