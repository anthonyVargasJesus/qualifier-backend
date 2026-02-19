using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.RiskStatus.Queries.GetRiskStatussByCompanyId
{
    public class GetRiskStatussByCompanyIdQuery : IGetRiskStatussByCompanyIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetRiskStatussByCompanyIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int skip, int pageSize, string search, int companyId)
        {
            try
            {
                var entities = await (from riskStatus in _databaseService.RiskStatus
                                      where ((riskStatus.isDeleted == null || riskStatus.isDeleted == false) && riskStatus.companyId == companyId)
                                      && (riskStatus.name.ToUpper().Contains(search.ToUpper()))
                                      select new RiskStatusEntity
                                      {
                                          riskStatusId = riskStatus.riskStatusId,
                                          name = riskStatus.name,
                                          description = riskStatus.description,
                                          abbreviation = riskStatus.abbreviation,
                                          value = riskStatus.value,
                                          color = riskStatus.color,
                                          companyId = riskStatus.companyId,
                                      })
                .Skip(skip).Take(pageSize)
                .ToListAsync();

                BaseResponseDto<GetRiskStatussByCompanyIdDto> baseResponseDto = new BaseResponseDto<GetRiskStatussByCompanyIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetRiskStatussByCompanyIdDto>>(entities);
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
            var total = await (from riskStatus in _databaseService.RiskStatus
                               where ((riskStatus.isDeleted == null || riskStatus.isDeleted == false) && riskStatus.companyId == companyId)
                               && (riskStatus.name.ToUpper().Contains(search.ToUpper()))
                               select new RiskStatusEntity
                               {
                                   riskStatusId = riskStatus.riskStatusId,
                               }).CountAsync();
            return total;
        }

    }
}

