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
        public async Task<Object> Execute(int skip, int pageSize, string search, int standardId)
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

                var entities = await (from requirement in _databaseService.Requirement
                                      where ((requirement.isDeleted == null || requirement.isDeleted == false)
                                      && requirement.standardId == standardId) && requirement.level == FIRST_LEVEL
                                      && (requirement.name.ToUpper().Contains(search.ToUpper()))
                                      select new RequirementEntity
                                      {
                                          requirementId = requirement.requirementId,
                                          numeration = requirement.numeration,
                                          name = requirement.name,
                                          description = requirement.description,
                                          level = requirement.level,
                                          parentId = requirement.parentId,
                                      }).OrderBy(x => x.numeration)
                                        .Skip(skip).Take(pageSize)
                                        .ToListAsync();


                foreach (var entity in entities)
                {

                }

                BaseResponseDto<GetRequirementsByStandardIdDto> baseResponseDto = new BaseResponseDto<GetRequirementsByStandardIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetRequirementsByStandardIdDto>>(entities);
                baseResponseDto.pagination = Pagination.GetPagination(await getTotal(search, standardId), pageSize);
                return baseResponseDto;
            }
            catch (Exception ex)
            {
                throw ex;
                //return BaseApplication.getExceptionErrorResponse();
            }
        }

        bool hasChildren(List<RequirementEntity> allRequirements, int idRequirement)
        {
            return allRequirements.Count(x => x.parentId == idRequirement) > 0;
        }

        //int children(List<RequirementEntity> allRequirements, int idRequirement)
        //{

        //    if ((num > 0) || (num <= 10))
        //        return (num * factorial(num - 1));
        //}

        //int factorial(int num)
        //{

        //    if ((num > 0) || (num <= 10))
        //        return (num * factorial(num - 1));
        //}

        public async Task<int> getTotal(string search, int standardId)
        {
            var total = await (from requirement in _databaseService.Requirement
                               where ((requirement.isDeleted == null || requirement.isDeleted == false) && requirement.standardId == standardId)
                               && (requirement.name.ToUpper().Contains(search.ToUpper()))
                               select new RequirementEntity
                               {
                                   requirementId = requirement.requirementId,
                               }).CountAsync();
            return total;
        }

    }
}

