using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.Requirement.Queries.GetRequirementsByStandardId
{
    public class GetRequirementsByStandardIdQuery : IGetRequirementsByStandardIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;

        public GetRequirementsByStandardIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }
        public async Task<Object> Execute(int standardId)
        {
            try
            {

                var allRequirements = await (from requirement in _databaseService.Requirement
                                             where ((requirement.isDeleted == null || requirement.isDeleted == false) && requirement.standardId == standardId)
                                             select new RequirementEntity
                                             {
                                                 requirementId = requirement.requirementId,
                                                 numeration = requirement.numeration,
                                                 name = requirement.name,
                                                 description = requirement.description,
                                                 level = requirement.level,
                                                 parentId = requirement.parentId,
                                             }).ToListAsync();

                const int FIRST_LEVEL = 1;
                const int SECOND_LEVEL = 2;
                const int THIRD_LEVEL = 3;

                var entities = await (from requirement in _databaseService.Requirement
                                      where ((requirement.isDeleted == null || requirement.isDeleted == false)
                                      && requirement.standardId == standardId && requirement.level == FIRST_LEVEL)
                                      select new RequirementEntity
                                      {
                                          requirementId = requirement.requirementId,
                                          numeration = requirement.numeration,
                                          name = requirement.name,
                                          description = requirement.description,
                                          level = requirement.level,
                                          parentId = requirement.parentId,
                                      }).OrderBy(x => x.numeration)
                                        .ToListAsync();     

                BaseResponseDto<GetRequirementsByStandardIdDto> baseResponseDto = new BaseResponseDto<GetRequirementsByStandardIdDto>();

                List<GetRequirementsByStandardIdDto> data = _mapper.Map<List<GetRequirementsByStandardIdDto>>(entities);

                foreach (var requirement in data)
                {
                    if (hasChildren(allRequirements, requirement.requirementId, SECOND_LEVEL))
                    {
                        requirement.children = getChildren(allRequirements, requirement.requirementId, SECOND_LEVEL).OrderBy(x => x.numeration).ToList();

                        foreach (var item in requirement.children)
                            if (hasChildren(allRequirements, item.requirementId, THIRD_LEVEL))
                                item.children = getChildren(allRequirements, item.requirementId, THIRD_LEVEL).OrderBy(x => x.numeration).ToList();  
                    }
                }

                baseResponseDto.data = data;

                return baseResponseDto;
            }
            catch (Exception)
            {
             return BaseApplication.getExceptionErrorResponse();
            }
        }

        bool hasChildren(List<RequirementEntity> allRequirements, int idRequirement, int level)
        {
            return allRequirements.Count(x => x.parentId == idRequirement && x.level == level) > 0;
        }
        List<GetRequirementsByStandardIdDto> getChildren(List<RequirementEntity> allRequirements, int idRequirement, int level)
        {
            var entities = allRequirements.Where(x => x.parentId == idRequirement && x.level == level).ToList();
            return _mapper.Map<List<GetRequirementsByStandardIdDto>>(entities);
        }

    }
}

