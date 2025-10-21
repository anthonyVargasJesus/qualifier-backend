using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.RequirementInDefaultRisk.Queries.GetAllRequirementInDefaultRisksByRequirementId
{
    internal class GetAllRequirementInDefaultRisksByRequirementIdQuery : IGetAllRequirementInDefaultRisksByRequirementIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        public GetAllRequirementInDefaultRisksByRequirementIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }

        public async Task<Object> Execute(int requirementId)
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
                                      }).ToListAsync();

                BaseResponseDto<GetAllRequirementInDefaultRisksByRequirementIdDto> baseResponseDto = new BaseResponseDto<GetAllRequirementInDefaultRisksByRequirementIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetAllRequirementInDefaultRisksByRequirementIdDto>>(entities);
                return baseResponseDto;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

