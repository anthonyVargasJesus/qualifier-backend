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
                //var allRequirements = await (from requirement in _databaseService.Requirement
                //                             where ((requirement.isDeleted == null || requirement.isDeleted == false) && requirement.standardId == standardId)
                //                             select new RequirementEntity
                //                             {
                //                                 requirementId = requirement.requirementId,
                //                                 numeration = requirement.numeration,
                //                                 name = requirement.name,
                //                                 description = requirement.description,
                //                                 level = requirement.level,
                //                                 parentId = requirement.parentId,
                //                                 letter = requirement.letter == null ? "" : requirement.letter

                //                             }).ToListAsync();

                //const int FIRST_LEVEL = 1;
                //const int SECOND_LEVEL = 2;
                //const int THIRD_LEVEL = 3;
                //const int FOURTH_LEVEL = 4;
                //const int FIFTH_LEVEL = 5;

                //var entities = await (from requirement in _databaseService.Requirement
                //                      where ((requirement.isDeleted == null || requirement.isDeleted == false)
                //                      && requirement.standardId == standardId && requirement.level == FIRST_LEVEL)
                //                      select new RequirementEntity
                //                      {
                //                          requirementId = requirement.requirementId,
                //                          numeration = requirement.numeration,
                //                          name = requirement.name,
                //                          description = requirement.description,
                //                          level = requirement.level,
                //                          parentId = requirement.parentId,
                //                          letter = requirement.letter == null ? "" : requirement.letter
                //                      }).OrderBy(x => x.numeration)
                //                        .ToListAsync();

                var requirements = await (from requirement in _databaseService.Requirement
                                          where ((requirement.isDeleted == null || requirement.isDeleted == false) && requirement.standardId == standardId
                                          && requirement.isEvaluable)
                                          select new RequirementEntity
                                          {
                                              requirementId = requirement.requirementId,
                                              numeration = requirement.numeration,
                                              name = requirement.name,
                                              description = requirement.description,
                                              level = requirement.level,
                                              parentId = requirement.parentId,
                                          }).ToListAsync();

                var standardEntity = new StandardEntity();
                standardEntity.setRequirementsWithChildren(requirements);

                BaseResponseDto<GetRequirementsByStandardIdDto> baseResponseDto = new BaseResponseDto<GetRequirementsByStandardIdDto>();
                List<GetRequirementsByStandardIdDto> data = _mapper.Map<List<GetRequirementsByStandardIdDto>>(standardEntity.requirements);

                //foreach (var requirement in data)
                //{
                //    if (hasChildren(allRequirements, requirement.requirementId, SECOND_LEVEL))
                //    {
                //        requirement.children = getChildren(allRequirements, requirement.requirementId, SECOND_LEVEL).OrderBy(x => x.numeration).ToList();

                //        foreach (var item in requirement.children)
                //            if (hasChildren(allRequirements, item.requirementId, THIRD_LEVEL))
                //            {
                //                item.children = getChildren(allRequirements, item.requirementId, THIRD_LEVEL).OrderBy(x => x.numeration).ToList();

                //                foreach(var item2 in item.children)
                //                    if (hasChildren(allRequirements, item2.requirementId, FOURTH_LEVEL))
                //                    {
                //                        item2.children = getChildren(allRequirements, item2.requirementId, FOURTH_LEVEL).OrderBy(x => x.numeration).ToList();

                //                        foreach (var item3 in item2.children)
                //                        {
                //                            if (hasChildren(allRequirements, item3.requirementId, FIFTH_LEVEL))
                //                            {
                //                                item3.children = getChildren(allRequirements, item3.requirementId, FIFTH_LEVEL).OrderBy(x => x.numeration).ToList();
                //                            }
                //                        }


                //                    }


                //            }
                                
                //    }
                //}


                //foreach (var item in data)
                //    item.numerationToShow = item.numeration.ToString();

                //foreach (var item in data)
                //    foreach (var item2 in data)
                //        if (item.parentId == item2.requirementId)
                //        {
                //            item.numerationToShow = item2.numeration + "." + item.numeration;
                //            foreach (var item3 in data)
                //                if (item2.parentId == item3.requirementId)
                //                    item.numerationToShow = item3.numeration + "." + item.numerationToShow;
                //        }

                //data = data.OrderBy(x => x.numerationToShow).ToList();


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

