using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.DefaultRisk.Queries.GetAllDefaultRisksByStandardId
{
    internal class GetAllDefaultRisksByStandardIdQuery : IGetAllDefaultRisksByStandardIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        public GetAllDefaultRisksByStandardIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }

        public async Task<Object> Execute(int standardId)
        {
            try
            {
                var entities = await (from defaultRisk in _databaseService.DefaultRisk
                                      join menace in _databaseService.Menace on defaultRisk.menace equals menace
                                      join vulnerability in _databaseService.Vulnerability on defaultRisk.vulnerability equals vulnerability
                                      where ((defaultRisk.isDeleted == null || defaultRisk.isDeleted == false) && defaultRisk.standardId == standardId)
                                      select new DefaultRiskEntity
                                      {
                                          defaultRiskId = defaultRisk.defaultRiskId,
                                          standardId = defaultRisk.standardId,
                                          name = defaultRisk.name,
                                          menaceId = defaultRisk.menaceId,
                                          vulnerabilityId = defaultRisk.vulnerabilityId,
                                          companyId = defaultRisk.companyId,
                                      }).OrderBy(e => e.name).ToListAsync();

                BaseResponseDto<GetAllDefaultRisksByStandardIdDto> baseResponseDto = new BaseResponseDto<GetAllDefaultRisksByStandardIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetAllDefaultRisksByStandardIdDto>>(entities);
                return baseResponseDto;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

