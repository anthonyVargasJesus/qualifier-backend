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
                                      }).ToListAsync();

                BaseResponseDto<GetAllRequirementsByStandardIdDto> baseResponseDto = new BaseResponseDto<GetAllRequirementsByStandardIdDto>();
                baseResponseDto.data = _mapper.Map<List<GetAllRequirementsByStandardIdDto>>(entities);
                return baseResponseDto;
            }
            catch (Exception)
            {
                return BaseApplication.getExceptionErrorResponse();
            }
        }

    }
}

