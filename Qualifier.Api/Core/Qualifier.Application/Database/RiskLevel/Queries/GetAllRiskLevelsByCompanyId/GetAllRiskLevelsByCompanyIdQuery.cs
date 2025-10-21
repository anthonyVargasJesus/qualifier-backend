using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.RiskLevel.Queries.GetAllRiskLevelsByCompanyId
{
    internal class GetAllRiskLevelsByCompanyIdQuery : IGetAllRiskLevelsByCompanyIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        public GetAllRiskLevelsByCompanyIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }

        public async Task<Object> Execute(int companyId)
        {
            try
            {
                var entities = await (from riskLevel in _databaseService.RiskLevel
                                      where ((riskLevel.isDeleted == null || riskLevel.isDeleted == false) && riskLevel.companyId == companyId)
                                      select new RiskLevelEntity
                                      {
                                          riskLevelId = riskLevel.riskLevelId,
                                          name = riskLevel.name,
                                          description = riskLevel.description,
                                          abbreviation = riskLevel.abbreviation,
                                          factor = riskLevel.factor,
                                          minimum = riskLevel.minimum,
                                          maximum = riskLevel.maximum,
                                          color = riskLevel.color,
                                          companyId = riskLevel.companyId,
                                      }).OrderBy(e => e.riskLevelId).ToListAsync();

                BaseResponseDto<GetAllRiskLevelsByCompanyIdDto> baseResponseDto = new BaseResponseDto<GetAllRiskLevelsByCompanyIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetAllRiskLevelsByCompanyIdDto>>(entities);
                return baseResponseDto;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

