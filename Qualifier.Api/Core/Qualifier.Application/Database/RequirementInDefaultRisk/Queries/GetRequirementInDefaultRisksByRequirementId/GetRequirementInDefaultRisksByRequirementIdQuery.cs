using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.RequirementInDefaultRisk.Queries.GetRequirementInDefaultRisksByRequirementId
{
    public class GetRequirementInDefaultRisksByRequirementIdQuery : IGetRequirementInDefaultRisksByRequirementIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetRequirementInDefaultRisksByRequirementIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int skip, int pageSize, string search, int requirementId)
        {
            try
            {
                var entities = await (from requirementInDefaultRisk in _databaseService.RequirementInDefaultRisk
                                      join defaultRisk in _databaseService.DefaultRisk on requirementInDefaultRisk.defaultRisk equals defaultRisk
                                      where ((requirementInDefaultRisk.isDeleted == null || requirementInDefaultRisk.isDeleted == false) && requirementInDefaultRisk.requirementId == requirementId)
                                      select new RequirementInDefaultRiskEntity
                                      {
                                          requirementInDefaultRiskId = requirementInDefaultRisk.requirementInDefaultRiskId,
                                          defaultRiskId = requirementInDefaultRisk.defaultRiskId,
                                          requirementId = requirementInDefaultRisk.requirementId,
                                          isActive = requirementInDefaultRisk.isActive,
                                          companyId = requirementInDefaultRisk.companyId,
                                          defaultRisk = new DefaultRiskEntity
                                          {
                                              name = defaultRisk.name,
                                          },
                                      })
                .Skip(skip).Take(pageSize)
                .ToListAsync();

                BaseResponseDto<GetRequirementInDefaultRisksByRequirementIdDto> baseResponseDto = new BaseResponseDto<GetRequirementInDefaultRisksByRequirementIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetRequirementInDefaultRisksByRequirementIdDto>>(entities);
                baseResponseDto.pagination = Pagination.GetPagination(await getTotal(search, requirementId), pageSize);
                return baseResponseDto;
            }
            catch (Exception)
            {
             return BaseApplication.getExceptionErrorResponse();
            }
        }

        public async Task<int> getTotal(string search, int requirementId)
        {
            var total = await (from requirementInDefaultRisk in _databaseService.RequirementInDefaultRisk
                               join defaultRisk in _databaseService.DefaultRisk on requirementInDefaultRisk.defaultRisk equals defaultRisk
                               where ((requirementInDefaultRisk.isDeleted == null || requirementInDefaultRisk.isDeleted == false) && requirementInDefaultRisk.requirementId == requirementId)
                               select new RequirementInDefaultRiskEntity
                               {
                                   requirementInDefaultRiskId = requirementInDefaultRisk.requirementInDefaultRiskId,
                               }).CountAsync();
            return total;
        }

    }
}

