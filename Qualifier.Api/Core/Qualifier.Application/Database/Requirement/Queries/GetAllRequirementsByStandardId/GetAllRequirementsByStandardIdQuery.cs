using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualifier.Common.Application.Dto;
using Qualifier.Common.Application.Service;
using Qualifier.Domain.Entities;

namespace Qualifier.Application.Database.Requirement.Queries.GetAllRequirementsByStandardId
{
    internal class GetAllRequirementsByStandardIdQuery : IGetAllRequirementsByStandardIdQuery
    {
        private readonly IDatabaseService _databaseService;
        private readonly IMapper _mapper;
        public GetAllRequirementsByStandardIdQuery(IDatabaseService databaseService, IMapper mapper)
        {
            _databaseService = databaseService;
            _mapper = mapper;
        }

        public async Task<Object> Execute(int standardId)
        {
            try
            {
                var entities = await (from requirement in _databaseService.Requirement
                                      where ((requirement.isDeleted == null || requirement.isDeleted == false) && requirement.standardId == standardId)
                                      select new RequirementEntity
                                      {
                                          requirementId = requirement.requirementId,
                                          numeration = requirement.numeration,
                                          name = requirement.name,
                                          level = requirement.level,
                                          parentId = requirement.parentId,
                                      }).ToListAsync();

                var data = _mapper.Map<List<GetAllRequirementsByStandardIdDto>>(entities);

                foreach (var item in data)
                    item.numerationToShow = item.numeration.ToString();

                foreach (var item in data)
                    foreach (var item2 in data)
                        if (item.parentId == item2.requirementId)
                        {
                            item.numerationToShow = item2.numeration + "." + item.numeration;
                            foreach (var item3 in data)
                                if (item2.parentId == item3.requirementId)
                                    item.numerationToShow = item3.numeration + "." + item.numerationToShow; 
                        }

                data = data.OrderBy(x => x.numerationToShow).ToList();

                BaseResponseDto<GetAllRequirementsByStandardIdDto> baseResponseDto = new BaseResponseDto<GetAllRequirementsByStandardIdDto>();
                baseResponseDto.data = data;
                return baseResponseDto;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

