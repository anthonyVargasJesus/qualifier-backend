using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Application.Cache;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.MaturityLevel.Queries.GetAllMaturityLevelsByCompanyId
{
    internal class GetAllMaturityLevelsByCompanyIdQuery : IGetAllMaturityLevelsByCompanyIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        private readonly IAppCacheService _cacheService;

        public static string CacheKey(int companyId) => $"maturityLevels:{companyId}";

        public GetAllMaturityLevelsByCompanyIdQuery(IDatabaseService databaseService, IMapper mapper, IAppCacheService cacheService)
        {
            _databaseService = databaseService;
            _mapper = mapper;
            _cacheService = cacheService;
        }

        public async Task<Object> Execute(int companyId)
        {
            try
            {
                // Niveles de madurez: se configuran una vez por empresa y casi nunca cambian —
                // se cachean por companyId e se invalidan explícitamente en los commands de
                // MaturityLevel (Create/Update/Delete).
                return await _cacheService.GetOrCreateAsync(CacheKey(companyId), async () =>
                {
                    var entities = await (from maturityLevel in _databaseService.MaturityLevel
                                          where ((maturityLevel.isDeleted == null || maturityLevel.isDeleted == false) && maturityLevel.companyId == companyId)
                                          select new MaturityLevelEntity
                                          {
                                              maturityLevelId = maturityLevel.maturityLevelId,
                                              name = maturityLevel.name,
                                              abbreviation = maturityLevel.abbreviation,
                                              value = maturityLevel.value,
                                              color = maturityLevel.color,
                                              generatesBreach = maturityLevel.generatesBreach,
                                              breachSeverityId = maturityLevel.breachSeverityId,
                                          })
                                           .OrderBy(e=>e.value)
                                          .ToListAsync();

                    BaseResponseDto<GetAllMaturityLevelsByCompanyIdDto> baseResponseDto = new BaseResponseDto<GetAllMaturityLevelsByCompanyIdDto>();
                    baseResponseDto.data = _mapper.Map<List<GetAllMaturityLevelsByCompanyIdDto>>(entities.OrderByDescending(x => x.value).ToList());
                    return (Object)baseResponseDto;
                });
            }
            catch (Exception)
            {
             return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

