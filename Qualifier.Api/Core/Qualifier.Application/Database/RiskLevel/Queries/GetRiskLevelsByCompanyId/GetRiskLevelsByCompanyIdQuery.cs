using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.RiskLevel.Queries.GetRiskLevelsByCompanyId
{
    public class GetRiskLevelsByCompanyIdQuery : IGetRiskLevelsByCompanyIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetRiskLevelsByCompanyIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int skip, int pageSize, string search, int companyId)
        {
            try
            {
                var entities = await (from riskLevel in _databaseService.RiskLevel
                                      where ((riskLevel.isDeleted == null || riskLevel.isDeleted == false) && riskLevel.companyId == companyId)
                                      && (riskLevel.name.ToUpper().Contains(search.ToUpper()))
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
                                      })
                .Skip(skip).Take(pageSize)
                .ToListAsync();

                BaseResponseDto<GetRiskLevelsByCompanyIdDto> baseResponseDto = new BaseResponseDto<GetRiskLevelsByCompanyIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetRiskLevelsByCompanyIdDto>>(entities);
                baseResponseDto.pagination = Pagination.GetPagination(await getTotal(search, companyId), pageSize);
                return baseResponseDto;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

        public async Task<int> getTotal(string search, int companyId)
        {
            var total = await (from riskLevel in _databaseService.RiskLevel
                               where ((riskLevel.isDeleted == null || riskLevel.isDeleted == false) && riskLevel.companyId == companyId)
                               && (riskLevel.name.ToUpper().Contains(search.ToUpper()))
                               select new RiskLevelEntity
                               {
                                   riskLevelId = riskLevel.riskLevelId,
                               }).CountAsync();
            return total;
        }

    }
}

