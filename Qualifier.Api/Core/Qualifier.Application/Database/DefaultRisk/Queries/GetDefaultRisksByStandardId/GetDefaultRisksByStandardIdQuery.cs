using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.DefaultRisk.Queries.GetDefaultRisksByStandardId
{
    public class GetDefaultRisksByStandardIdQuery : IGetDefaultRisksByStandardIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetDefaultRisksByStandardIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int skip, int pageSize, string search, int standardId)
        {
            try
            {
                var entities = await (from defaultRisk in _databaseService.DefaultRisk
                                      join menace in _databaseService.Menace on defaultRisk.menace equals menace
                                      join vulnerability in _databaseService.Vulnerability on defaultRisk.vulnerability equals vulnerability
                                      where ((defaultRisk.isDeleted == null || defaultRisk.isDeleted == false) && defaultRisk.standardId == standardId)
                                      && (defaultRisk.name.ToUpper().Contains(search.ToUpper()))
                                      select new DefaultRiskEntity
                                      {
                                          defaultRiskId = defaultRisk.defaultRiskId,
                                          standardId = defaultRisk.standardId,
                                          name = defaultRisk.name,
                                          menaceId = defaultRisk.menaceId,
                                          vulnerabilityId = defaultRisk.vulnerabilityId,
                                          companyId = defaultRisk.companyId,
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

                BaseResponseDto<GetDefaultRisksByStandardIdDto> baseResponseDto = new BaseResponseDto<GetDefaultRisksByStandardIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetDefaultRisksByStandardIdDto>>(entities);
                baseResponseDto.pagination = Pagination.GetPagination(await getTotal(search, standardId), pageSize);
                return baseResponseDto;
            }
            catch (Exception)
            {
             return BaseApplication.getExceptionErrorResponse();
            }
        }

        public async Task<int> getTotal(string search, int standardId)
        {
            var total = await (from defaultRisk in _databaseService.DefaultRisk
                               join menace in _databaseService.Menace on defaultRisk.menace equals menace
                               join vulnerability in _databaseService.Vulnerability on defaultRisk.vulnerability equals vulnerability
                               where ((defaultRisk.isDeleted == null || defaultRisk.isDeleted == false) && defaultRisk.standardId == standardId)
                               && (defaultRisk.name.ToUpper().Contains(search.ToUpper()))
                               select new DefaultRiskEntity
                               {
                                   defaultRiskId = defaultRisk.defaultRiskId,
                               }).CountAsync();
            return total;
        }

    }
}

